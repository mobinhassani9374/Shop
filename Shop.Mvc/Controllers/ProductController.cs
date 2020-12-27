﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Shop.Mvc.Mapping;
using Shop.Mvc.Models.Product;
using Shop.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Shop.Mvc.Controllers
{
    public class ProductController : BaseController
    {
        private UserService _userService;
        public ProductController(UserService userService)
        {
            _userService = userService;
        }
        public IActionResult Detail(int id)
        {
            var serviceResult = _userService.GetProductDetail(id);
            if (serviceResult.IsSuccess)
            {
                ViewBag.Comments = _userService.GetAllAcepteCommentForProduct(id).ToViewModel();
            }
            return View_Get(serviceResult.Data?.ToViewModel(), "/");
        }

        [HttpPost]
        [ValidateAntiForgeryToken()]
        [Authorize()]
        public IActionResult AddComment(AddCommentViewModel model)
        {
            var serviceResult = _userService.AddCommentForProduct(model.ToDto(), UserId);
            return View_Get(serviceResult, $"{nameof(Detail)}/{model.ProductId}");
        }
    }
}
