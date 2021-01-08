using System;
using System.Collections.Generic;
using System.Text;

namespace Shop.Domain.Entities
{
    public class OrderDetail
    {
        public int Id { get; set; }
        public Product Product { get; set; }
        public int ProductId { get; set; }
        public long Price { get; set; }
        public int Count { get; set; }
        public Order Order { get; set; }
        public int OrderId { get; set; }
        public long DisCount { get; set; }
    }
}
