using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Shop.Mvc.Mapping;
using Shop.Mvc.Models.Cart;
using Shop.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Shop.Mvc.Controllers
{
    public class CartController : BaseController
    {
        private readonly UserService _userService;
        public CartController(UserService userService)
        {
            _userService = userService;
        }
        [HttpPost]
        [Authorize()]
        public IActionResult Add(AddToCartViewModel model)
        {
            var serviceResult = _userService.AddToCart(model.ToDto(UserId));
            if (serviceResult.IsSuccess)
            {
                var carts = _userService.GetCarts(UserId).ToViewModel();
                var cartServiceResult = serviceResult.AddData<List<CartViewModel>>(carts);
                return Json(cartServiceResult);
            }
            return Json(serviceResult);
        }
    }
}
