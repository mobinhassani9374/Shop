﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Shop.Domain.Entities
{
    public class Product
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public long Price { get; set; }
        public int Count { get; set; }
        public string PrimaryImage { get; set; }
        public int CategoryId { get; set; }
        public Category Category { get; set; }
    }
}