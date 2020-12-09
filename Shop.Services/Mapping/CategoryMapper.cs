using Shop.Domain.Dto.Category;
using Shop.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Shop.Services.Mapping
{
    public static class CategoryMapper
    {
        public static Category ToEntity(this CreateCategoryDto source)
        {
            return new Category
            {
                ParentId = source.ParentId,
                Title = source.Title
            };
        }

        public static CategoryDto ToDto(this Category source)
        {
            return new CategoryDto
            {
                Id = source.Id,
                ParentId = source.ParentId,
                Title = source.Title
            };
        }
    }
}
