using System;
using System.Collections.Generic;
using System.Text;

namespace Shop.Domain.Dto.Home
{
    public class ProductDto
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public long Price { get; set; }
        public int Count { get; set; }
        public string PrimaryImage { get; set; }
    }
}
