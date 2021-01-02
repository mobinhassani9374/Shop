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
    public class CategoryController : BaseController
    {
        private readonly UserService _userService;
        public CategoryController(UserService userService)
        {
            _userService = userService;
        }
        public IActionResult Index(ProductUserSearchViewModel searchModel)
        {
            var serviceResult = _userService.GetProducts(searchModel.ToDto());
            if (!serviceResult.IsSuccess)
            {
                Swal(false, "دسته بندی یافت نشد");
                return RedirectPermanent("/");
            }
            return View_Search(searchModel, serviceResult.Data.ToViewModel());
        }
        public IActionResult Get()
        {
            var data = _userService.GetAllCategories();
            return Json(data.ToViewModel());
        }
    }
}
