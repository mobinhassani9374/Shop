﻿using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Shop.Mvc.Models.Product
{
    public class CreateProductViewModel
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public IFormFile ImageFile { get; set; }
        public int CategoryId { get; set; }
        public long Price { get; set; }
        public int Count { get; set; }
    }
}