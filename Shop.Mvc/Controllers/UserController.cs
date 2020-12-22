using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Shop.Mvc.Controllers
{
    [Authorize()]
    public class UserController : BaseController
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
