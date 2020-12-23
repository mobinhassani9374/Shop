using Shop.Domain.Dto.SlideShow;
using Shop.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Shop.Services.Mapping
{
    public static class SlideShowMapping
    {
        public static SlideShow ToEntity(this CreateSlideShowDto source, string imageName)
        {
            return new SlideShow
            {
                Link = source.Link,
                ImageName = imageName
            };
        }
    }
}
