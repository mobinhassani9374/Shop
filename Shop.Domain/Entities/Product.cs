using System;
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
        public string ImagesJson { get; set; }
        public int CategoryId { get; set; }
        public Category Category { get; set; }
        public bool IsAmazing { get; set; }
        public long Discount { get; set; }
        public string Garanty { get; set; }
        public string Attributes { get; set; }
        public string GarantyCondition { get; set; }
        public ICollection<Cart> Carts { get; set; }
        public ICollection<OrderDetail> OrderDetails { get; set; }
        public ICollection<ProductVote> Votes { get; set; }
    }
}
