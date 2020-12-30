using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Shop.Domain.Dto.Home
{
    public class CreateContactUsDto
    {
        [Required(ErrorMessage = "پیشنهادات و انتقادات نمی تواند فاقد مقدار باشد")]
        public string Description { get; set; }
        public string UserId { get; set; }
    }
}
