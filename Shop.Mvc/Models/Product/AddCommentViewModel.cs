﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Shop.Mvc.Models.Product
{
    public class AddCommentViewModel
    {
        public string Comment { get; set; }
        public int ProductId { get; set; }
    }
}
