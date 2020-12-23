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
        public static List<SlideShowViewModel> ToViewModel(this List<SlideShowDto> sources)
        {
            var result = new List<SlideShowViewModel>();
            foreach (var source in sources)
                result.Add(source.ToViewModel());
            return result;
        }

        public static SlideShowViewModel ToViewModel(this SlideShowDto source)
        {
            return new SlideShowViewModel
            {
                Id = source.Id,
                ImageName = source.ImageName,
                Link = source.Link
            };
        }
    }
}
