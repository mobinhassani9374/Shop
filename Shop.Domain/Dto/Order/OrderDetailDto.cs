using Shop.Domain.Dto.Product;
using System;
using System.Collections.Generic;
using System.Text;

namespace Shop.Domain.Dto.Order
{
    public class OrderDetailDto
    {
        public int Id { get; set; }
        public ProductDto Product { get; set; }
        public int ProductId { get; set; }
        public long Price { get; set; }
        public int Count { get; set; }
        public int OrderId { get; set; }
        public long DisCount { get; set; }
    }
}
