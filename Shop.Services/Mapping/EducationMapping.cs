using Shop.Domain.Dto.Educations;
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
    }
}
