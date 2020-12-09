using System;
using System.Collections.Generic;
using System.Text;

namespace Shop.Domain.Dto.Category
{
    public class CreateCategoryDto
    {
        public int? ParentId { get; set; }
        public string Title { get; set; }
    }
}
