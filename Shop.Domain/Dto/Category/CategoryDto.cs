using System;
using System.Collections.Generic;
using System.Text;

namespace Shop.Domain.Dto.Category
{
    public class CategoryDto
    {
        public int Id { get; set; }
        public int? ParentId { get; set; }
        public string Title { get; set; }
        public List<CategoryDto> Children { get; set; }
    }
}
