using System;
using System.Collections.Generic;
using System.Text;

namespace Shop.Domain.Entities
{
    public class Category
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public ICollection<Category> Children { get; set; }
        public Category Parent { get; set; }
        public int? ParentId { get; set; }
    }
}
