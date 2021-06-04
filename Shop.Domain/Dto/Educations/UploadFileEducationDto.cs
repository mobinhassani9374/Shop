using Microsoft.AspNetCore.Http;
using Shop.Domain.Enumeration;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Shop.Domain.Dto.Educations
{
    public class UploadFileEducationDto
    {
        public int EducationId { get; set; }

        [MaxLength(Constants.Product_Title_Length, ErrorMessage = "تعداد کاراکترهای عنوان نمی تواند بیش از @ کاراکتر باشد")]
        public string Title { get; set; }

        [Required(ErrorMessage = "فایل نمی تواند فاقد مقدار باشد")]
        public IFormFile File { get; set; }
        public EducationFileType Type { get; set; }

        public string FileName { get; set; }
    }
}
