using Shop.Domain.Dto.SlideShow;
using Shop.Mvc.Models.SlideShow;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Shop.Mvc.Mapping
{
    public static class SlideShowMapping
    {
        public static CreateSlideShowDto ToDto(this CreateSlideShowViewModel source)
        {
            return new CreateSlideShowDto
            {
                Link = source.Link,
                ImageFile = source.ImageFile
            };
        }
    }
}
