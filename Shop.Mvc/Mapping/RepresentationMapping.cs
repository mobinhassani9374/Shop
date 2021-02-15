using Shop.Domain.Dto.Representations;
using Shop.Mvc.Models.Representations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Shop.Mvc.Mapping
{
    public static class RepresentationMapping
    {
        public static RepresentationCreateDto ToDto(this RepresentationCreateViewModel source)
        {
            return new RepresentationCreateDto
            {
                WhatsAppNumber = source.WhatsAppNumber,
                Address = source.Address,
                FullName = source.FullName,
                InstagramAccount = source.InstagramAccount,
                PhoneNumber = source.PhoneNumber,
                Title = source.Title
            };
        }
        public static List<RepresentationViewModel> ToViewModel(this List<RepresentationDto> sources)
        {
            var result = new List<RepresentationViewModel>();
            foreach (var source in sources)
                result.Add(source.ToViewModel());
            return result;
        }
        public static RepresentationViewModel ToViewModel(this RepresentationDto source)
        {
            return new RepresentationViewModel
            {
                Address = source.Address,
                FullName = source.FullName,
                Id = source.Id,
                InstagramAccount = source.InstagramAccount,
                PhoneNumber = source.PhoneNumber,
                Title = source.Title,
                WhatsAppNumber = source.WhatsAppNumber
            };
        }
    }
}
