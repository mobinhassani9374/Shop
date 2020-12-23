using Microsoft.AspNetCore.Mvc;
using Shop.Mvc.Mapping;
using Shop.Mvc.Models.Order;
using Shop.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Shop.Mvc.Areas.Admin.Controllers
{
    public class OrderController : AdminBaseController
    {
        private readonly AdminService _adminService;
        public OrderController(AdminService adminService)
        {
            _adminService = adminService;
        }
        public IActionResult Index(OrderSearchViewModel searchModel)
        {
            var data = _adminService.GetOrders(searchModel.ToDto());
            return View_Search(searchModel, data.ToViewModel());
        }
    }
}
