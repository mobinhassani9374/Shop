using Shop.Domain.Dto.Educations;
using Shop.Domain.Dto.Pagination;
using Shop.Mvc.Models;
using Shop.Mvc.Models.Educations;
using Shop.Mvc.Models.Pagination;
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
        public static SearchEducationDto ToDto(this EducationSearchViewModel source)
        {
            return new SearchEducationDto
            {
                PageNumber = source.PageNumber,
                PageSize = source.PageSize
            };
        }

        public static PaginationViewModel<EducationViewModel> ToViewModel(this PaginationDto<EducationDto> source)
        {
            return new PaginationViewModel<EducationViewModel>
            {
                PageSize = source.PageSize,
                Count = source.Count,
                PageCount = source.PageCount,
                PageNumber = source.PageNumber,
                Data = source.Data.ToViewModel()
            };
        }
        public static List<EducationViewModel> ToViewModel(this List<EducationDto> sources)
        {
            var list = new List<EducationViewModel>();
            foreach (var source in sources)
                list.Add(source.ToViewModel());
            return list;
        }

        public static EducationViewModel ToViewModel(this EducationDto source)
        {
            return new EducationViewModel
            {
                Id = source.Id,
                Image = source.Image,
                Title = source.Title
            };
        }
    }
}
