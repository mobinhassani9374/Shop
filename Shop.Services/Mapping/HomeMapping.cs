using Shop.Domain.Dto.Home;
using Shop.Domain.Dto.Pagination;
using Shop.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Shop.Services.Mapping
{
    public static class HomeMapping
    {
        public static ContactUs ToEntity(this CreateContactUsDto source)
        {
            return new ContactUs
            {
                Description = source.Description,
                UserId = source.UserId
            };
        }

        public static PaginationDto<ContactUsDto> ToDto(this PaginationDto<ContactUs> source)
        {
            return new PaginationDto<ContactUsDto>()
            {
                Count = source.Count,
                Data = source.Data.ToDto(),
                PageCount = source.PageCount,
                PageNumber = source.PageNumber,
                PageSize = source.PageSize
            };
        }
        public static List<ContactUsDto> ToDto(this List<ContactUs> sources)
        {
            var result = new List<ContactUsDto>();
            foreach (var source in sources)
                result.Add(source.ToDto());
            return result;
        }
        public static ContactUsDto ToDto(this ContactUs source)
        {
            return new ContactUsDto
            {
                Description = source.Description,
                UserId = source.UserId,
                Id = source.Id,
                Date = source.Date
            };
        }
    }
}
