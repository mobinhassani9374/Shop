using Microsoft.AspNetCore.Mvc;
using Shop.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Shop.Mvc.Controllers
{
    public class BaseController : Controller
    {
        protected void AddErrors(ServiceResult serviceResult)
        {
            foreach (var error in serviceResult.Errors)
                ModelState.AddModelError("", error);
        }
        protected void Swal(bool isSuccess, string message)
        {
            TempData.Clear();
            TempData.Add("serviceResult.Message", message);
            TempData.Add("serviceResult.Success", isSuccess);
        }
        protected string UserId
        {
            get
            {
                var userClaim = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier);
                return userClaim?.Value;
            }
        }
    }
}
