using Shop.Domain.Dto.SlideShow;
using System;
using System.Collections.Generic;
using System.Text;

namespace Shop.Domain.Dto.Home
{
    public class UserIndexDto
    {
        public List<CategoryDto> ProductCategries { get; set; }
        public List<SlideShowDto> SlideShows { get; set; }
    }
}
