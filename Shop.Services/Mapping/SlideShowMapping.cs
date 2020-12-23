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

        public static List<SlideShowDto> ToDto(this List<SlideShow> sources)
        {
            var result = new List<SlideShowDto>();
            foreach (var source in sources)
                result.Add(source.ToDto());
            return result;
        }

        public static SlideShowDto ToDto(this SlideShow source)
        {
            return new SlideShowDto
            {
                Id = source.Id,
                ImageName = source.ImageName,
                Link = source.Link
            };
        }
    }
}
