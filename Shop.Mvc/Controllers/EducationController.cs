using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Shop.Mvc.Mapping;
using Shop.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Shop.Mvc.Controllers
{
    public class EducationController : Controller
    {
        private readonly UserService _userService;
        private readonly IHostingEnvironment _hostingEnvironment;
        public EducationController(UserService userService,
            IHostingEnvironment hostingEnvironment)
        {
            _userService = userService;
            _hostingEnvironment = hostingEnvironment;
        }
        public IActionResult Detail(int id)
        {
            var data = _userService.GetAllEducationFiles(id);
            return View(data.ToViewModel());
        }

        public IActionResult Download(int id)
        {
            var data = _userService.GetAllEducationFile(id);
            if (data == null) return Content("شناسه وارد شده معتبر نیست");

            var relativePath = System.IO.Path.Combine(_hostingEnvironment.WebRootPath, "Files", "EducationFiles", data.FileName);

            if (System.IO.File.Exists(relativePath))
            {
                _userService.IncreaseCountDownload(id);
                return PhysicalFile(relativePath, "mobin/mobin", data.FileName);
            }
            else
            {
                return Content("مشگلی پیش آمد");
            }
        }
    }
}
