using Microsoft.AspNetCore.Mvc;
using Shop.Domain.Dto.UserAccess;
using Shop.Mvc.Mapping;
using Shop.Mvc.Models.Pagination;
using Shop.Mvc.Models.UserManagement;
using Shop.Services;
using Shop.Utility;
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
        public IActionResult Index(SearchUserViewModel searchModel)
        {
            var data = _adminService.GetUsers(searchModel.ToDto());
            return View_Search(searchModel, data.ToViewModel());
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
            return View_Post(serviceResult, model);
        }

        public async Task<IActionResult> Permision(string userId)
        {
            var response = await _adminService.GetGroupingAccess(userId);
            return View_Delete(response, response.Data?.ToViewModel(), nameof(Index));
        }
    }
}
