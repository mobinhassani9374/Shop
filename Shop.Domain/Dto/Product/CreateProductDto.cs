﻿using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;

namespace Shop.Domain.Dto.Product
{
    public class CreateProductDto
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public IFormFile ImageFile { get; set; }
        public string ImageName { get; set; }
        public int CategoryId { get; set; }
        public long Price { get; set; }
        public int Count { get; set; }
    }
}