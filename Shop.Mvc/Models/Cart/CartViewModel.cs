using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Shop.Mvc.Models.Cart
{
    public class CartViewModel
    {
        public int ProductId { get; set; }
        public string ProductTitle { get; set; }
        public long Price { get; set; }
        public int Count { get; set; }
    }
}
