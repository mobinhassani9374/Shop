using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Shop.Services;

namespace Shop.Mvc.Areas.Admin.Controllers
{
    public class HomeController : AdminBaseController
    {
        private readonly AdminService _adminService;
        public HomeController(AdminService adminService)
        {
            _adminService = adminService;
        }
        public IActionResult Index()
        {
            ViewBag.CommentWatingCount = _adminService.GetCommentWatingCount();
            return View();
        }
    }
}
