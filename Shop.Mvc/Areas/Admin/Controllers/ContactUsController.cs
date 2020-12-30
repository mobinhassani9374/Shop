using Microsoft.AspNetCore.Mvc;
using Shop.Mvc.Mapping;
using Shop.Mvc.Models.Home;
using Shop.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Shop.Mvc.Areas.Admin.Controllers
{
    public class ContactUsController : AdminBaseController
    {
        private readonly AdminService _adminService;
        public ContactUsController(AdminService adminService)
        {
            _adminService = adminService;
        }
        public IActionResult Index(ContactUSSearchViewModel searchModel)
        {
            var data = _adminService.GetContactUs(searchModel.ToDto());
            return View_Search(searchModel, data.ToViewModel());
        }
    }
}
