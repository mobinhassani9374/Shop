using System;
using System.Collections.Generic;
using System.Text;

namespace Shop.Domain.Dto.Cart
{
    public class CartDto
    {
        public int ProductId { get; set; }
        public string ProductTitle { get; set; }
        public string ProductImage { get; set; }
        public long Price { get; set; }
        public int Count { get; set; }
    }
}
