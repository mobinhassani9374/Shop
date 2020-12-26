using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Shop.Domain.Dto.Product
{
    public class AddProductCommentDto
    {
        [Required(ErrorMessage = "نظر نمی تواند فاقد مقدار باشد")]
        public string Comment { get; set; }
        public int ProductId { get; set; }
    }
}
