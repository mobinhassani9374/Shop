using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Shop.Mvc.Models.Home
{
    public class ProductViewModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public long Price { get; set; }
        public int Count { get; set; }
        public string PrimaryImage { get; set; }
        public bool IsAmazing { get; set; }
        public long Discount { get; set; }
    }
}
