using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Shop.Mvc.Attributes;
using Shop.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using Shop.Utility.Extensions;

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
        protected IActionResult View_Post<T>(ServiceResult serviceResult, T model)
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
        protected IActionResult View_Search<TSearch, TModel>(TSearch search, TModel model)
        {
            var selectItemProperties = search.GetType().GetProperties()
                .Where(c => c.CustomAttributes
                .Any(i => i.AttributeType == typeof(SelectItemAttribute)))
                .ToList();

            foreach (var prop in selectItemProperties)
            {
                var enumValues = Enum.GetValues(prop.PropertyType);
                List<SelectListItem> selectListItem = new List<SelectListItem>();
                foreach (var value in enumValues)
                {
                    var enumSource = (Enum)Enum.Parse(prop.PropertyType, value.ToString());
                    selectListItem.Add(new SelectListItem(enumSource.GetDisplayName(), value.ToString(), prop.GetValue(search).ToString() == value.ToString()));
                }
                ViewData.Add($"{prop.Name}", selectListItem);
            }

            return View(new Models.SearchModel<TSearch, TModel>(search, model));
        }
        protected IActionResult View_Delete<TModel>(ServiceResult serviceResult, TModel model, string redirectToAction)
        {
            if (!serviceResult.IsSuccess)
            {
                Swal(false, serviceResult.Errors.FirstOrDefault());
                return RedirectToAction(redirectToAction);
            }

            return View(model);
        }
        protected IActionResult View_Get<TModel>(TModel model, string redirectToAction)
        {
            if (model == null)
            {
                Swal(false, "شناسه ارسالی نامعتبر است");
                return RedirectToAction(redirectToAction);
            }

            return View(model);
        }
        protected IActionResult View_Get(ServiceResult serviceResult, string redirectToAction)
        {
            if (serviceResult.IsSuccess)
                Swal(true, "عملیات با موفقت انجام شد");
            else Swal(false, serviceResult.Errors.FirstOrDefault());

            return RedirectToAction(redirectToAction);
        }
    }
}
