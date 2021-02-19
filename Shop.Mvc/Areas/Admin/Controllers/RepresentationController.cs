using Microsoft.AspNetCore.Mvc;
using Shop.Mvc.Mapping;
using Shop.Mvc.Models.Representations;
using Shop.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Shop.Mvc.Areas.Admin.Controllers
{
    public class RepresentationController : AdminBaseController
    {
        private readonly AdminService _adminService;
        public RepresentationController(AdminService adminService)
        {
            _adminService = adminService;
        }

        public IActionResult Index()
        {
            var data = _adminService.GetAllRepresentations();
            return View(data.ToViewModel());
        }
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken()]
        public IActionResult Create(RepresentationCreateViewModel model)
        {
            var serviceResult = _adminService.CreateRepresentation(model.ToDto());
            return View_Post(serviceResult, model);
        }

        public IActionResult Edit(int id)
        {
            var data = _adminService.GetRepresentation(id);
            return View_Get(data?.ToEditViewModel(), nameof(Index));
        }

        [HttpPost]
        [ValidateAntiForgeryToken()]
        public IActionResult Edit(RepresentationEditViewModel model)
        {
            var serviceResult = _adminService.EditRepresentation(model.ToDto());
            return View_Post(serviceResult, model);
        }
    }
}
