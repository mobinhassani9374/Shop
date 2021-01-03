using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Shop.Database.Identity.Entities;
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
        private readonly SignInManager<User> _signInManager;
        public UserController(UserService userService,
            SignInManager<User> signInManager)
        {
            _userService = userService;
            _signInManager = signInManager;
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

        public IActionResult EditProfile()
        {
            var currentUser = _userService.GetUser(UserId);
            return View(currentUser.ToEditProfile());
        }

        [HttpPost]
        [ValidateAntiForgeryToken()]
        public async Task<IActionResult> EditProfile(EditProfileViewModel model)
        {
            var serviceResult = _userService.EditProfile(model.ToDto(UserId));
            if (serviceResult.IsSuccess)
            {
                await _signInManager.SignOutAsync();
                await _signInManager.SignInAsync(_userService.GetUser(UserId), true);
            }
            return View_Post(serviceResult, model);
        }
    }
}
