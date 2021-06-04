using Microsoft.AspNetCore.Http;
using Shop.Domain.Enumeration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Shop.Mvc.Models.Educations
{
    public class UploadFileEducationViewModel
    {
        public int EducationId { get; set; }
        public string Title { get; set; }
        public IFormFile File { get; set; }
        public EducationFileType Type { get; set; }
    }
}
