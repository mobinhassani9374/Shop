using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Shop.Mvc.Models.Home
{
    public class CategoryViewModel
    {
        public int Id { get; set; }
        public string CategoryTitle { get; set; }
        public List<ProductViewModel> Products { get; set; }
    }
}
