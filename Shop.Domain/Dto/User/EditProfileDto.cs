using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Shop.Domain.Dto.User
{
    public class EditProfileDto
    {
        [Required(ErrorMessage = "نام و نام خانوادگی شما نمی تواند فاقد مقدار باشد")]
        public string FullName { get; set; }
        public string UserId { get; set; }
    }
}
