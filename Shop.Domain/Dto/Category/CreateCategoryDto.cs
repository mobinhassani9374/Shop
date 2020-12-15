using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Shop.Domain.Dto.Category
{
    public class CreateCategoryDto
    {
        public int? ParentId { get; set; }

        [Required(ErrorMessage = "عنوان دسته بندی نمی تواند فاقد مقدار باشد")]
        [MaxLength(Constants.Category_Title_Length, ErrorMessage = "تعداد کاراکترهای عنوان نمی تواند بیش از @ کاراکتر باشد")]
        public string Title { get; set; }
    }
}
