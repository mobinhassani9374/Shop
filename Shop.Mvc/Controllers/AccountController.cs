using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Shop.Database;
using Shop.Database.Identity.Entities;
using Shop.Domain.Dto.Cart;
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

        public IActionResult Login(string returnUrl)
        {
            if (User.Identity.IsAuthenticated)
            {
                Swal(false, "نمی توانید به صفحه ورود دسترسی داشته باشید");
                return RedirectPermanent("/");
            }
            ViewBag.ReturnUrl = returnUrl;
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken()]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (string.IsNullOrEmpty(model.ReturnUrl))
                model.ReturnUrl = "/";

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
                     .PasswordSignInAsync(user.UserName, model.Password, isPersistent: true, lockoutOnFailure: false);

                    if (signInResult.Succeeded)
                    {
                        AddToCart(model.ReturnUrl);
                        if (model.ReturnUrl.ToLower().Contains("addtocart"))
                            model.ReturnUrl = "/";
                        return RedirectPermanent(model.ReturnUrl);
                    }
                }
                else serviceResult.AddError("کاربری یافت نشد");
            }

            Swal(false, serviceResult.Errors.FirstOrDefault());

            return View(model);
        }

        public IActionResult Register(string returnUrl)
        {
            if (User.Identity.IsAuthenticated)
            {
                Swal(false, "نمی توانید به صفحه ثبت نام دسترسی داشته باشید");
                return RedirectPermanent("/");
            }
            ViewBag.ReturnUrl = returnUrl;
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken()]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            if (string.IsNullOrEmpty(model.ReturnUrl))
                model.ReturnUrl = "/";

            var serviceResult = await _userService.Register(model.ToDto());
            if (serviceResult.IsSuccess)
            {
                var signInResult = await _signInManager
                   .PasswordSignInAsync(model.PhoneNumber, model.Password, true, false);

                if (signInResult.Succeeded)
                {
                    Swal(true, $"خوش آمدید {model.FullName} عزیز");
                    AddToCart(model.ReturnUrl);
                    if (model.ReturnUrl.ToLower().Contains("addtocart"))
                        model.ReturnUrl = "/";
                    return RedirectPermanent(model.ReturnUrl);
                }

            }
            return View_Post(serviceResult, model);
        }

        public async Task<IActionResult> Logout()
        {
            if (User.Identity.IsAuthenticated)
                await _signInManager.SignOutAsync();
            return RedirectPermanent("/");
        }

        private void AddToCart(string returnUrl)
        {
            returnUrl = returnUrl.ToLower();
            if (returnUrl.Contains("addtocart"))
            {
                var productIdStr = returnUrl.Remove(returnUrl.IndexOf("addtocart="), 10);
                int productId = 0;
                int.TryParse(productIdStr, out productId);
                if (productId > 0)
                    _userService.AddToCart(new AddToCartDto { ProductId = productId, UserId = UserId });
            }
        }

        public IActionResult ForgotPassword()
        {
            if (User.Identity.IsAuthenticated)
            {
                Swal(false, "نمی توانید به صفحه مربوطه دسترسی داشته باشید");
                return RedirectPermanent("/");
            }

            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken()]
        public async Task<IActionResult> ForgotPassword(ForgotPasswordViewModel model)
        {
            var serviceResult = await _userService.ForgotPassword(model.PhoneNumber);
            if (serviceResult.IsSuccess)
            {
                serviceResult.Message = "رمزعبور جدید برایتان ارسال شد";
                return RedirectToAction(nameof(Login));
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
