﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Shop.Mvc.Mapping;
using Shop.Mvc.Models.Product;
using Shop.Services;
using Shop.Utility;
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

        public IActionResult Index(ProductSearchViewModel searchModel)
        {
            var data = _adminService.GetProducts(searchModel.ToDto());
            return View_Search(searchModel, data.ToViewModel());
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

        public IActionResult Edit(int id)
        {
            var data = _adminService.GetProduct(id);
            return View_Get(data?.ToViewModel(), nameof(Index));
        }

        [HttpPost]
        [ValidateAntiForgeryToken()]
        public IActionResult Edit(UpdateProductViewModel model)
        {
            var serviceResult = _adminService.UpdateProduct(model.ToDto());
            return View_Post(serviceResult, model);
        }

        public IActionResult Delete(int id)
        {
            var serviceResult = _adminService.DeleteProduct(id);
            return View_Get(serviceResult, nameof(Index));
        }

        public IActionResult Images(int id)
        {
            ViewBag.Id = id;
            var serviceResult = _adminService.GetAllImagesForProduct(id);
            return View_Get(serviceResult.Data, nameof(Index));
        }

        [HttpPost]
        [ValidateAntiForgeryToken()]
        public IActionResult AddNewImage(int id, List<IFormFile> images)
        {
            foreach (var image in images)
            {
                _adminService.AddNewImage(id, image);
            }
                
            var serviceResult = new ServiceResult(true);
            serviceResult.Message = "عملیات با موفقیت صورت گرفت";
            return View_Get(serviceResult, $"{nameof(Images)}/{id}");
        }

        public IActionResult DeleteImage(int id, string imageGuid)
        {
            var serviceResult = _adminService.DeleteImageForProduct(id, imageGuid);
            return View_Get(serviceResult, $"{nameof(Images)}/{id}");
        }

        public IActionResult CommentWating()
        {
            return View(_adminService.GetAllWatingComments().ToViewModel());
        }

        public IActionResult AcceptComment(int id)
        {
            var serviceResult = _adminService.AcceptCommentForProduct(id);
            return View_Get(serviceResult, nameof(CommentWating));
        }

        public IActionResult CancelComment(int id)
        {
            var serviceResult = _adminService.CancelCommentForProduct(id);
            return View_Get(serviceResult, nameof(CommentWating));
        }
    }
}
