using Shop.Mvc.Models.Product;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Shop.Mvc.Models.Order
{
    public class OrderDetailViewModel
    {
        public int Id { get; set; }
        public ProductViewModel Product { get; set; }
        public int ProductId { get; set; }
        public long Price { get; set; }
        public int Count { get; set; }
        public int OrderId { get; set; }
        public long DisCount { get; set; }
    }
}
