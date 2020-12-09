using Microsoft.AspNetCore.Mvc;
using Shop.Mvc.Mapping;
using Shop.Mvc.Models.CategoryManagement;
using Shop.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Shop.Mvc.Areas.Admin.Controllers
{
    public class CategoryManagementController : AdminBaseController
    {
        private readonly AdminService _adminService;
        public CategoryManagementController(AdminService adminService)
        {
            _adminService = adminService;
        }

        [HttpPost]
        public IActionResult Create(CreateCategoryViewModel model)
        {
            var sericeResult = _adminService.CreateCategory(model.ToDto());
            return Json(sericeResult.ToViewModel());
        }

        [HttpPost]
        public IActionResult Delete(int id)
        {
            var serviceResult = _adminService.DeleteCategory(id);
            return Json(serviceResult);
        }

        [HttpPost]
        public IActionResult Update(UpdateCategoryViewModel model)
        {
            var serviceResult = _adminService.UpdateCategory(model.ToDto());
            return Json(serviceResult);
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Get()
        {
            var data = _adminService.GetAllCategories();
            return Json(data.ToViewModel());
        }
    }
}
