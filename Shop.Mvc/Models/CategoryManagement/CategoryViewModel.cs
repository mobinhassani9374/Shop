using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Shop.Mvc.Models.CategoryManagement
{
    public class CategoryViewModel
    {
        public int Id { get; set; }
        public int? ParentId { get; set; }
        public string Title { get; set; }
        public List<CategoryViewModel> Children { get; set; }
        public CategoryViewModel Parent { get; set; }
    }
}
