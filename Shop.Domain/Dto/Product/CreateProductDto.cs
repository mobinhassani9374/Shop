using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Shop.Domain.Dto.Product
{
    public class CreateProductDto
    {
        [Required(ErrorMessage = "عنوان محصول نمی تواند فاقد مقدار باشد")]
        [MaxLength(Constants.Product_Title_Length, ErrorMessage = "تعداد کاراکترهای عنوان نمی تواند بیش از @ کاراکتر باشد")]
        public string Title { get; set; }

        [Required(ErrorMessage = "توضیحات محصول نمی تواند فاقد مقدار باشد")]
        [MaxLength(Constants.Product_Description_Length, ErrorMessage = "تعداد کاراکترهای توضیحات نمی تواند بیش از @ کاراکتر باشد")]
        public string Description { get; set; }

        [Required(ErrorMessage = "عکس محصول نمی تواند فاقد مقدار باشد")]
        public IFormFile ImageFile { get; set; }

        public string ImageName { get; set; }

        public int CategoryId { get; set; }

        public long Price { get; set; }

        public int Count { get; set; }
    }
}
