using Shop.Domain.Dto.Educations;
using Shop.Domain.Dto.Pagination;
using Shop.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Shop.Services.Mapping
{
    public static class EducationMapping
    {
        public static Education ToEntity(this EducationCreateDto source)
        {
            return new Education
            {
                Image = source.ImageName,
                Title = source.Title
            };
        }
        public static PaginationDto<EducationDto> ToDto(this PaginationDto<Education> sources)
        {
            return new PaginationDto<EducationDto>
            {
                Count = sources.Count,
                PageCount = sources.PageCount,
                PageNumber = sources.PageNumber,
                PageSize = sources.PageSize,
                Data = sources.Data.ToDto()
            };
        }
        public static List<EducationDto> ToDto(this List<Education> sources)
        {
            var list = new List<EducationDto>();
            foreach (var sourc in sources)
                list.Add(sourc.ToDto());
            return list;
        }
        public static EducationDto ToDto(this Education source)
        {
            return new EducationDto
            {
                Id = source.Id,
                Image = source.Image,
                Title = source.Title
            };
        }
    }
}
