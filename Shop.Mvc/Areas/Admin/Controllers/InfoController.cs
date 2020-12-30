using Microsoft.AspNetCore.Mvc;
using Shop.Mvc.Cache;
using Shop.Mvc.Mapping;
using Shop.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Shop.Mvc.Areas.Admin.Controllers
{
    public class InfoController : AdminBaseController
    {
        private readonly AdminService _adminService;
        public InfoController(AdminService adminService)
        {
            _adminService = adminService;
        }
        public IActionResult Index()
        {
            return View(_adminService.GetLastInfo().ToViewModel());
        }

        [HttpPost]
        [ValidateAntiForgeryToken()]
        public IActionResult Index(Models.Info.InfoViewModel model)
        {
            var serviceResult = _adminService.SaveInfo(model.ToDto());
            if (serviceResult.IsSuccess)
                CacheManager.SetInfo(_adminService.GetLastInfo()
                    .ToViewModel());

            return View_Get(serviceResult, nameof(Index));
        }
    }
}
