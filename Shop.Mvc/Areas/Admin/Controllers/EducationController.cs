using Microsoft.AspNetCore.Mvc;
using Shop.Mvc.Mapping;
using Shop.Mvc.Models.Educations;
using Shop.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Shop.Mvc.Areas.Admin.Controllers
{
    public class EducationController : AdminBaseController
    {
        private readonly AdminService _adminService;
        public EducationController(AdminService adminService)
        {
            _adminService = adminService;
        }
        public IActionResult Index(EducationSearchViewModel searchModel)
        {
            var data = _adminService.GetEducations(searchModel.ToDto());
            return View_Search(searchModel, data.ToViewModel());
        }
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken()]
        public IActionResult Create(EducationCreateViewModel model)
        {
            var serviceResult = _adminService.CreateEducation(model.ToDto());
            return View_Post(serviceResult, model);
        }
    }
}
