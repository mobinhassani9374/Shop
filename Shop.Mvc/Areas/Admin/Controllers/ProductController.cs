﻿using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Shop.Mvc.Areas.Admin.Controllers
{
    public class ProductController : AdminBaseController
    {
        public IActionResult Create()
        {
            return View();
        }
    }
}
