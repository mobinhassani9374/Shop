using Microsoft.AspNetCore.Mvc;
using Shop.Services;
using System.Net.Http;
using System.Threading.Tasks;

namespace Shop.Mvc.Controllers
{
    public class PaymentController : BaseController
    {
        private readonly UserService _userService;
        private readonly IHttpClientFactory _httpClientFactory;
        public PaymentController(UserService userService,
            IHttpClientFactory httpClientFactory)
        {
            _userService = userService;
            _httpClientFactory = httpClientFactory;
        }
        public async Task<IActionResult> Index()
        {
            var serviceResult = _userService.ConvertCartToOrder(UserId);
            if (serviceResult.IsSuccess)
            {
                var user = _userService.GetUser(UserId);

                var payment = new Services.Payment.IdPay.Payment(_httpClientFactory.CreateClient());

                var obj = new Services.Payment.IdPay.Payment.Request(serviceResult.Data.Id.ToString());
                obj.amount = decimal.Parse(serviceResult.Data.TotalPrice.ToString());
                obj.name = user.FullName;
                obj.phone = user.PhoneNumber;
                obj.mail = "";
                obj.desc = $"خربد توسط  {user.FullName}";

                var res = await payment.RequestPayment(obj);

                if (res is Services.Payment.IdPay.Payment.RequestRespons_Success)
                {
                    var sucRes = ((Services.Payment.IdPay.Payment.RequestRespons_Success)res);
                    _userService.UpdateOrder(serviceResult.Data.Id, sucRes.id);
                    return RedirectPermanent(sucRes.link);
                }
                else
                {
                    var failRes = ((Services.Payment.IdPay.Payment.RequestRespons_Fail)res).error_message;
                }
            }
            return View(serviceResult.Data);
        }

        public async Task<IActionResult> Success(int id)
        {
            await _userService.SuccessPayment(id);
            return RedirectPermanent("/");
        }
    }
}
