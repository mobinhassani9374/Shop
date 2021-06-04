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
        public static EducationFile ToEntity(this UploadFileEducationDto source)
        {
            return new EducationFile
            {
                CountDownload = 0,
                FileName = source.FileName,
                Length = source.File.Length,
                Title = source.Title,
                Type = source.Type,
                EducationId = source.EducationId
            };
        }
        public static List<EducationFileDto> ToDto(this List<EducationFile> sources)
        {
            var list = new List<EducationFileDto>();
            foreach (var source in sources)
                list.Add(source.ToDto());
            return list;
        }

        public static EducationFileDto ToDto(this EducationFile source)
        {
            return new EducationFileDto
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
    }
}
