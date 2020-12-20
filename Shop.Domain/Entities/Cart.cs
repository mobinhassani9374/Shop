using System;
using System.Collections.Generic;
using System.Text;

namespace Shop.Domain.Entities
{
    public class Cart
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public string UserId { get; set; }
        public int ProductId { get; set; }
        public Product Product { get; set; }
    }
}
