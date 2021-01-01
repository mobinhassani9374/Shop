using Microsoft.AspNetCore.Mvc;
using Shop.Mvc.Mapping;
using Shop.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Shop.Mvc.Controllers
{
    public class CategoryController : BaseController
    {
        private readonly UserService _userService;
        public CategoryController(UserService userService)
        {
            _userService = userService;
        }
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Get()
        {
            var data = _userService.GetAllCategories();
            return Json(data.ToViewModel());
        }
    }
}
