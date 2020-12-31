using Microsoft.AspNetCore.Mvc;
using Shop.Services;
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

        public async Task<IActionResult> Success(int id)
        {
            await _userService.SuccessPayment(id);
            return RedirectPermanent("/");
        }
    }
}
