using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Shop.Mvc.Models.SlideShow
{
    public class CreateSlideShowViewModel
    {
        public string Link { get; set; }
        public IFormFile ImageFile { get; set; }
    }
}
