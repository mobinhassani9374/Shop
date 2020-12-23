using Microsoft.AspNetCore.Mvc;
using Shop.Mvc.Mapping;
using Shop.Mvc.Models.SlideShow;
using Shop.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Shop.Mvc.Areas.Admin.Controllers
{
    public class SlideShowController : AdminBaseController
    {
        public AdminService _adminService;
        public SlideShowController(AdminService adminService)
        {
            _adminService = adminService;
        }
        public IActionResult Index()
        {
            return View(_adminService.GetAllSlideShows().ToViewModel());
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken()]
        public IActionResult Create(CreateSlideShowViewModel model)
        {
            var servieResult = _adminService.CreateSlideShow(model.ToDto());
            return View_Post(servieResult, model);
        }

        public IActionResult Delete(int id)
        {
            var serviceResult = _adminService.DeleteSlideShow(id);
            return View_Get(serviceResult, nameof(Index));
        }
    }
}
