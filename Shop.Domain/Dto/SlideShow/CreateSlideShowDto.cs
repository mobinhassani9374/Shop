using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Shop.Domain.Dto.SlideShow
{
    public class CreateSlideShowDto
    {
        public string Link { get; set; }

        [Required(ErrorMessage = "عکس محصول نمی تواند فاقد مقدار باشد")]
        public IFormFile ImageFile { get; set; }
    }
}
