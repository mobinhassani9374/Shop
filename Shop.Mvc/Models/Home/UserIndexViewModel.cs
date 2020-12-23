using Shop.Mvc.Models.SlideShow;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Shop.Mvc.Models.Home
{
    public class UserIndexViewModel
    {
        public List<CategoryViewModel> ProductCategries { get; set; }

        public List<SlideShowViewModel> SlideShows { get; set; }
    }
}
