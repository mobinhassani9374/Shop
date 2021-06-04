using Shop.Domain.Enumeration;
using System;
using System.Collections.Generic;
using System.Text;

namespace Shop.Domain.Dto.Educations
{
    public class EducationFileDto
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string FileName { get; set; }
        public long Length { get; set; }
        public EducationFileType Type { get; set; }
        public int CountDownload { get; set; }
        public int EducationId { get; set; }
    }
}
