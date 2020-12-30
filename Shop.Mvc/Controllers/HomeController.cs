﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Shop.Mvc.Mapping;
using Shop.Mvc.Models.Home;
using Shop.Services;

namespace Shop.Mvc.Controllers
{
    public class HomeController : BaseController
    {
        private readonly UserService _userService;
        public HomeController(UserService userService)
        {
            _userService = userService;
        }
        public IActionResult Index()
        {
            return View(_userService.GetUserIndexData().ToViewModel());
        }

        public IActionResult HelpForBuy()
        {
            return View();
        }

        public IActionResult AboutUs()
        {
            return View();
        }
        public IActionResult LawUs()
        {
            return View();
        }

        public IActionResult ContactUs()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize()]
        public IActionResult ContactUs(CreateContactUsViewModel model)
        {
            var serviceResult = _userService.CreateContactUs(model.ToDto(UserId));
            return View_Get(serviceResult, nameof(ContactUs));
        }
    }
}
