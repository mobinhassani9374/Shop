using Shop.Domain.Dto.Category;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Shop.Mvc.Models.CategoryManagement
{
    public class CreateCategoryViewModel
    {
        public int? ParentId { get; set; }
        public string Title { get; set; }
    }
}
