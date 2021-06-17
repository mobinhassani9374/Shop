using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Shop.Domain.Enumeration;
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

        public IActionResult UploadFile(int id)
        {
            if (!_adminService.ExistEducation(id))
            {
                Swal(false, "شناسه آموزش اشتباه می باشد");
                return RedirectToAction(nameof(Index));
            }

            List<SelectListItem> typeSelector = new List<SelectListItem>();
            typeSelector.Add(new SelectListItem("توع فایل را انتخاب کنید", ""));
            typeSelector.Add(new SelectListItem("عکس", EducationFileType.Image.ToString()));
            typeSelector.Add(new SelectListItem("صوت", EducationFileType.Audio.ToString()));
            typeSelector.Add(new SelectListItem("ویدیو", EducationFileType.Video.ToString()));
            typeSelector.Add(new SelectListItem("فایل فشرده", EducationFileType.Zip.ToString()));

            ViewBag.TypeSelector = typeSelector;
            ViewBag.EducationId = id;

            ViewBag.Education = _adminService.GetEducations(id)?.ToViewModel();

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken()]
        public IActionResult UploadFile(UploadFileEducationViewModel model)
        {
            var serviceResult = _adminService.UploadFileForEducation(model.ToDto());

            List<SelectListItem> typeSelector = new List<SelectListItem>();
            typeSelector.Add(new SelectListItem("توع فایل را انتخاب کنید", ""));
            typeSelector.Add(new SelectListItem("عکس", EducationFileType.Image.ToString()));
            typeSelector.Add(new SelectListItem("صوت", EducationFileType.Audio.ToString()));
            typeSelector.Add(new SelectListItem("ویدیو", EducationFileType.Video.ToString()));
            typeSelector.Add(new SelectListItem("فایل فشرده", EducationFileType.Zip.ToString()));

            ViewBag.TypeSelector = typeSelector;
            ViewBag.EducationId = model.EducationId;

            ViewBag.Education = _adminService.GetEducations(model.EducationId)?.ToViewModel();

            return View_Post(serviceResult, model);
        }

        public IActionResult Files(int id)
        {
            var data = _adminService.GetAllEducationFiles(id);
            return View(data.ToViewModel());
        }

        public IActionResult DeleteFile(int id)
        {
            var serviceResult = _adminService.DeleteEducationFile(id);

            if (serviceResult.IsSuccess)
                Swal(true, "عملیات با موفقیت صورت گرفت");
            else Swal(false, serviceResult.Errors.FirstOrDefault());

            return RedirectToAction(nameof(Files), new { id = serviceResult.Data });
        }

        public IActionResult Delete(int id)
        {
            var serviceResult = _adminService.DeleteEducation(id);
            if (serviceResult.IsSuccess)
                Swal(true, serviceResult.Message);
            else Swal(false, serviceResult.Errors.FirstOrDefault());

            return RedirectToAction(nameof(Index));
        }
    }
}
