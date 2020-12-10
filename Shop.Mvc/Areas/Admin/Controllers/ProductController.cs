using Microsoft.AspNetCore.Mvc;
using Shop.Mvc.Mapping;
using Shop.Mvc.Models.Product;
using Shop.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Shop.Mvc.Areas.Admin.Controllers
{
    public class ProductController : AdminBaseController
    {
        private readonly AdminService _adminService;
        public ProductController(AdminService adminService)
        {
            _adminService = adminService;
        }
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken()]
        public IActionResult Create(CreateProductViewModel model)
        {
            var sericeResult = _adminService.CreateProduct(model.ToDto());
            return View_Post(sericeResult, model);
        }
    }
}
