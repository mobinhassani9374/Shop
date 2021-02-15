using Shop.Domain.Dto.Representations;
using Shop.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Shop.Services.Mapping
{
    public static class RepresentationMapping
    {
        public static Representation ToEntity(this RepresentationCreateDto source)
        {
            return new Representation
            {
                Address = source.Address,
                FullName = source.FullName,
                InstagramAccount = source.InstagramAccount,
                PhoneNumber = source.PhoneNumber,
                Title = source.Title,
                WhatsAppNumber = source.WhatsAppNumber
            };
        }

        public static List<RepresentationDto> ToDto(this List<Representation> sources)
        {
            var result = new List<RepresentationDto>();
            foreach (var source in sources)
                result.Add(source.ToDto());
            return result;
        }
        public static RepresentationDto ToDto(this Representation source)
        {
            return new RepresentationDto
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
