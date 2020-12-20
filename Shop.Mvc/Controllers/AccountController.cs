﻿using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Shop.Database;
using Shop.Database.Identity.Entities;
using Shop.Mvc.Mapping;
using Shop.Mvc.Models.Account;
using Shop.Services;
using Shop.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Shop.Mvc.Controllers
{
    public class AccountController : BaseController
    {
        private readonly UserManager<User> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly SignInManager<User> _signInManager;
        private readonly AppDbContext _dbContext;
        private readonly UserService _userService;

        public AccountController(UserManager<User> userManager,
            RoleManager<IdentityRole> roleManager,
            SignInManager<User> signInManager,
            AppDbContext dbContext,
            UserService userService)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _signInManager = signInManager;
            _dbContext = dbContext;
            _userService = userService;
        }

        [Route("login")]
        public IActionResult Login()
        {
            return View();
        }

        [Route("login")]
        [HttpPost]
        [ValidateAntiForgeryToken()]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            var serviceResult = new ServiceResult(true);

            var user = _dbContext.Users.FirstOrDefault(c => c.PhoneNumber == model.PhoneNumber);

            if (user == null)
                serviceResult.AddError("کاربری یافت نشد");

            if (serviceResult.IsSuccess)
            {
                var checkPass = await _signInManager.CheckPasswordSignInAsync(user, model.Password, true);

                if (checkPass.Succeeded)
                {
                    var signInResult = await _signInManager
                     .PasswordSignInAsync(user.UserName, model.Password, true, false);

                    if (signInResult.Succeeded)
                    {
                        return RedirectPermanent("/admin");
                    }
                }
                else serviceResult.AddError("کاربری یافت نشد");
            }

            Swal(false, serviceResult.Errors.FirstOrDefault());

            return View(model);
        }

        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken()]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            var serviceResult = await _userService.Register(model.ToDto());
            if (serviceResult.IsSuccess)
            {
                var signInResult = await _signInManager
                   .PasswordSignInAsync(model.PhoneNumber, model.Password, true, false);

                if (signInResult.Succeeded)
                {
                    Swal(true, $"خوش آمدید {model.FullName} عزیز");
                    return RedirectPermanent("/");
                }

            }
            return View_Post(serviceResult, model);
        }

        /// sync
        public IActionResult Sync()
        {
            new DatabaseInitializer().Seed(_userManager, _roleManager);
            return Json(true);
        }
    }
}
