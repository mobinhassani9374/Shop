using Shop.Domain.Dto.Category;
using Shop.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
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

        public static Category ToEntity(this UpdateCategoryDto source, int? parentId)
        {
            return new Category
            {
                Id = source.Id,
                Title = source.Title,
                ParentId = parentId
            };
        }

        public static CategoryDto ToDto(this Category source)
        {
            return new CategoryDto
            {
                Id = source.Id,
                ParentId = source.ParentId,
                Title = source.Title,
                Children = source.Children?.ToList().ToDto()
            };
        }

        public static List<CategoryDto> ToDto(this List<Category> sources)
        {
            var result = new List<CategoryDto>();
            foreach (var source in sources)
                result.Add(source.ToDto());
            return result;
        }
    }
}
