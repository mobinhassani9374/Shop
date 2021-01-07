﻿using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Shop.Mvc.Models.Product
{
    public class UpdateProductViewModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public IFormFile ImageFile { get; set; }
        public int CategoryId { get; set; }
        public long Price { get; set; }
        public int Count { get; set; }
        public bool IsAmazing { get; set; }
        public long Discount { get; set; }
    }
}
