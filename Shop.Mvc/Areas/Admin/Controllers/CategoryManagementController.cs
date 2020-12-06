using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Shop.Mvc.Areas.Admin.Controllers
{
    public class CategoryManagementController : AdminBaseController
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Get()
        {
            var data = new List<Models.CategoryManagement.CategoryViewModel>();
            data.Add(new Models.CategoryManagement.CategoryViewModel
            {
                Id = 1,
                Text = "سیستم های حفاظتی و ایمنی",
                Children = new List<Models.CategoryManagement.CategoryViewModel>()
                   {
                    new Models.CategoryManagement.CategoryViewModel
                    {
                        Id=2,
                         Text="دزدگیر اماکن",
                          Children=new List<Models.CategoryManagement.CategoryViewModel>
                          {
                          new Models.CategoryManagement.CategoryViewModel
                          {
                              Id=5,
                               Text="پکیچ های آماده نصب"
                          },
                          new Models.CategoryManagement.CategoryViewModel
                          {
                           Text="دستگاه های دزدگیر مرکزی",
                            Id=6
                          },
                          new Models.CategoryManagement.CategoryViewModel
                          {
                           Id=7,
                            Text="سنسورها"
                          }
                          }
                    }
                   }
            });
            return Json(data);
        }
    }
}
