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
                Title = source.Title,
                 Description= source.Description,
                Files = source.Files?.ToViewModel()
            };
        }
        public static  EducationSetDetailViewModel ToDetailViewModel(this EducationDto source)
        {
            return new EducationSetDetailViewModel
            {
                Id = source.Id,
                Description = source.Description,
            };
        }

        
        public static UploadFileEducationDto ToDto(this UploadFileEducationViewModel source)
        {
            return new UploadFileEducationDto
            {
                EducationId = source.EducationId,
                File = source.File,
                Title = source.Title,
                Type = source.Type
            };
        }
        public static List<EducationFileViewModel> ToViewModel(this List<EducationFileDto> sources)
        {
            var list = new List<EducationFileViewModel>();
            foreach (var source in sources)
                list.Add(source.ToViewModel());
            return list;
        }

        public static EducationFileViewModel ToViewModel(this EducationFileDto source)
        {
            return new EducationFileViewModel
            {
                CountDownload = source.CountDownload,
                EducationId = source.EducationId,
                FileName = source.FileName,
                Id = source.Id,
                Length = source.Length,
                Title = source.Title,
                Type = source.Type
            };
        }

        public static EducationSetDetailDto ToDto(this EducationSetDetailViewModel source)
        {
            return new EducationSetDetailDto
            {
                 Description= source.Description,
                  Id= source.Id
            };
        }
    }
}
