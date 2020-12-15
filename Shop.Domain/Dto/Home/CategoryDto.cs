using System;
using System.Collections.Generic;
using System.Text;

namespace Shop.Domain.Dto.Home
{
    public class CategoryDto
    {
        public int Id { get; set; }
        public string CategoryTitle { get; set; }
        public List<ProductDto> Products { get; set; }
    }
}
