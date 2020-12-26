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
            return View_Get(serviceResult.Data?.ToViewModel(), "/");
        }

        [HttpPost]
        [ValidateAntiForgeryToken()]
        public IActionResult AddComment(AddCommentViewModel model)
        {
            return View();
        }
    }
}
