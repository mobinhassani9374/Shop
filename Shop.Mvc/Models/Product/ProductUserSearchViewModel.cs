using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Shop.Mvc.Models.Product
{
    public class ProductUserSearchViewModel : PagingViewModel
    {
        public string Title { get; set; }
        public int CategoryId { get; set; }
    }
}
