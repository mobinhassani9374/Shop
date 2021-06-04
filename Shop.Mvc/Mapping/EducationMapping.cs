using Shop.Domain.Dto.Educations;
using Shop.Mvc.Models.Educations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Shop.Mvc.Mapping
{
    public static class EducationMapping
    {
        public static EducationCreateDto ToDto(this EducationCreateViewModel source)
        {
            return new EducationCreateDto
            {
                Image = source.Image,
                Title = source.Title
            };
        }
    }
}
