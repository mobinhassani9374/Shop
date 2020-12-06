using Microsoft.AspNetCore.Mvc;
using Shop.Mvc.Attributes;
using Shop.Utility;
using System;
using System.Linq;
using System.Security.Claims;

namespace Shop.Mvc.Controllers
{
    public class BaseController : Controller
    {
        void AddErrors(ServiceResult serviceResult)
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
        protected IActionResult View<T>(ServiceResult serviceResult, T model)
        {
            if (!serviceResult.IsSuccess)
            {
                AddErrors(serviceResult);
                ViewBag.ServiceResult = serviceResult;
                return View(model);
            }
            else
            {
                Swal(true, "عملیات با موفقیت صورت گرفت");
                return RedirectToAction(this.ControllerContext.ActionDescriptor.ActionName);
            }
        }
        protected IActionResult View<TSearch, TModel>(TSearch search, TModel model)
        {
            var selectItemProperties = search.GetType().GetProperties()
                .Where(c => c.CustomAttributes
                .Any(i => i.AttributeType == typeof(SelectItemAttribute)))
                .ToList();

            foreach (var prop in selectItemProperties)
            {
                var attr = prop.CustomAttributes
                    .FirstOrDefault(c => c.AttributeType == typeof(SelectItemAttribute));

                var consut = attr.ConstructorArguments.Select(c => c.Value).First();

                var values = Enum.GetValues(consut.GetType());
            }

            return View(new Models.SearchModel<TSearch, TModel>(search, model));
        }
    }
}
