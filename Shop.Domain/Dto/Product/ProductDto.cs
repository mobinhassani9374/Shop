using Shop.Domain.Dto.Category;
using System;
using System.Collections.Generic;
using System.Text;

namespace Shop.Domain.Dto.Product
{
    public class ProductDto
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public long Price { get; set; }
        public int Count { get; set; }
        public string PrimaryImage { get; set; }
        public List<string> MoreImages { get; set; }
        public int CategoryId { get; set; }
        public bool IsAmazing { get; set; }
        public long Discount { get; set; }
        public string Garanty { get; set; }
        public string Attributes { get; set; }
        public CategoryDto Category { get; set; }
    }
}
