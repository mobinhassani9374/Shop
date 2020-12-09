using Shop.Domain.Dto.Category;
using Shop.Mvc.Models.CategoryManagement;
using Shop.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Shop.Mvc.Mapping
{
    public static class CategoryMapping
    {
        public static CreateCategoryDto ToDto(this CreateCategoryViewModel source)
        {
            return new CreateCategoryDto
            {
                Title = source.Title,
                ParentId = source.ParentId
            };
        }
        public static CategoryViewModel ToViewModel(this CategoryDto source)
        {
            return new CategoryViewModel
            {
                Id = source.Id,
                ParentId = source.ParentId,
                Title = source.Title,
                Parent = source.Parent?.ToViewModel(),
                Children = source.Children?.ToList().ToViewModel()
            };

        }
        public static List<CategoryViewModel> ToViewModel(this List<CategoryDto> sources)
        {
            var result = new List<CategoryViewModel>();
            foreach (var source in sources)
                result.Add(source.ToViewModel());
            return result;
        }
        public static ServiceResult<CategoryViewModel> ToViewModel(this ServiceResult<CategoryDto> source)
        {
            return
                new ServiceResult<CategoryViewModel>(source.IsSuccess,
                source.Data.ToViewModel());
        }
        public static UpdateCategoryDto ToDto(this UpdateCategoryViewModel source)
        {
            return new UpdateCategoryDto
            {
                Title = source.Title,
                Id = source.Id
            };
        }
    }
}
