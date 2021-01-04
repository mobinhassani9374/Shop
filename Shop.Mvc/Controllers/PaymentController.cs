using Microsoft.AspNetCore.Mvc;
using Shop.Services;
using Shop.Services.Payment.IdPay;
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

        [HttpPost]
        [ValidateAntiForgeryToken()]
        public async Task<IActionResult> Index(string address)
        {
            var failRes = "";
            var serviceResult = _userService.ConvertCartToOrder(UserId, address);
            if (serviceResult.IsSuccess)
            {
                var user = _userService.GetUser(UserId);

                var payment = new Payment(_httpClientFactory.CreateClient());

                var obj = new Payment.Request(serviceResult.Data.Id.ToString());
                obj.amount = decimal.Parse($"{serviceResult.Data.TotalPrice.ToString()}0");
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
                    failRes = ((Services.Payment.IdPay.Payment.RequestRespons_Fail)res).error_message;
                }
            }
            return Content(failRes);
        }

        public async Task<IActionResult> CallBack(int status, int track_id, string id, string order_id)
        {
            var serviceResult = new Utility.ServiceResult<string>(true);
            try
            {
                var payment = new Payment(_httpClientFactory.CreateClient());
                var obj = new Payment.ResultPayment();
                obj.status = status;
                obj.track_id = track_id.ToString();
                obj.id = id;
                obj.order_id = order_id;

                if (!obj.IsOK)
                    serviceResult.AddError("پرداختی صورت نگرفت");
                else
                {
                    // تایید تراکنش
                    var res = await payment.VerifyPayment(obj);
                    if (res is Payment.PaymentInfo)
                    {
                        var tmp = (Payment.PaymentInfo)res;
                        await _userService.SuccessPayment(int.Parse(order_id));
                        serviceResult.Data = order_id;
                    }
                    else
                    {
                        var errorMessage = ((Payment.RequestRespons_Fail)res).error_message;
                        serviceResult.AddError(errorMessage);
                    }

                }
            }
            catch (System.Exception ex)
            {
                serviceResult.AddError(ex.Message);
            }

            return View(serviceResult);
        }

        public async Task<IActionResult> Success(int id)
        {
            await _userService.SuccessPayment(id);
            return RedirectPermanent("/");
        }
    }
}
