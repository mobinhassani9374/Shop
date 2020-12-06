using Microsoft.AspNetCore.Mvc;
using Shop.Mvc.Mapping;
using Shop.Mvc.Models.UserManagement;
using Shop.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Shop.Mvc.Areas.Admin.Controllers
{
    public class UserManagementController : AdminBaseController
    {
        private readonly AdminService _adminService;
        public UserManagementController(AdminService adminService)
        {
            _adminService = adminService;
        }
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken()]
        public async Task<IActionResult> Create(CreateUserViewModel model)
        {
            var serviceResult = await _adminService.CreateUser(model.ToDto());
            return View(serviceResult, model);
        }

        public IActionResult Permision()
        {
            return View();
        }
    }
}
