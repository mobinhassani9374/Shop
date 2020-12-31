using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Shop.Mvc.Mapping;
using Shop.Mvc.Models.UserManagement;
using Shop.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Shop.Mvc.Controllers
{
    [Authorize()]
    public class UserController : BaseController
    {
        private readonly UserService _userService;
        public UserController(UserService userService)
        {
            _userService = userService;
        }
        public IActionResult Index()
        {
            var myOrders = _userService.GetMyOrders(UserId).ToViewModel();
            ViewBag.MyOrders = myOrders;
            return View();
        }
        public IActionResult ChangePassword()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken()]
        public async Task<IActionResult> ChangePassword(ChangePasswordModel model)
        {
            var serviceResult = await _userService.ChangePassword(model.ToDto(UserId));
            return View_Post(serviceResult, model);
        }
    }
}
