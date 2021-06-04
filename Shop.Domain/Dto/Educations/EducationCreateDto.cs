using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Shop.Domain.Dto.Educations
{
    public class EducationCreateDto
    {
        [Required(ErrorMessage = "عنوان آموزش نمی تواند فاقد مقدار باشد")]
        [MaxLength(Constants.Product_Title_Length, ErrorMessage = "تعداد کاراکترهای عنوان نمی تواند بیش از @ کاراکتر باشد")]
        public string Title { get; set; }

        [Required(ErrorMessage = "عکس نمی تواند فاقد مقدار باشد")]
        public IFormFile Image { get; set; }

        public string ImageName { get; set; }
    }
}
