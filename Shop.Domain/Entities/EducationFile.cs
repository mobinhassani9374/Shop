using Shop.Domain.Enumeration;
using System;
using System.Collections.Generic;
using System.Text;

namespace Shop.Domain.Entities
{
    public class EducationFile
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string FileName { get; set; }
        public long Length { get; set; }
        public EducationFileType Type { get; set; }
        public int CountDownload { get; set; }

        public Education Education { get; set; }
        public int EducationId { get; set; }
    }
}
