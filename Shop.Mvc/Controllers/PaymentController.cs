using Microsoft.AspNetCore.Mvc;
using Shop.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Shop.Mvc.Controllers
{
    public class PaymentController : BaseController
    {
        private readonly UserService _userService;
        public PaymentController(UserService userService)
        {
            _userService = userService;
        }
        public IActionResult Index()
        {
            var serviceResult = _userService.ConvertCartToOrder(UserId);
            return View(serviceResult.Data);
        }

        public IActionResult Success(int id)
        {
            _userService.SuccessPayment(id);
            return RedirectPermanent("/");
        }
    }
}
