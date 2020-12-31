using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Shop.Domain.Dto.User
{
    public class ChangePasswordDto
    {
        public string UserId { get; set; }

        [Required(ErrorMessage = "رمزعبور قبلی نمی تواند فاقد باشد")]
        public string OldPassword { get; set; }

        [Required(ErrorMessage = "رمزعبور جدید نمی تواند فاقد باشد")]
        [MinLength(Constants.Password_Length, ErrorMessage = "رمزعبور جدید نمی تواند کمتر از @ کاراکتر باشد")]
        public string NewPassword { get; set; }

        [Required(ErrorMessage = "تکرار رمز عبور جدید نمی تواند فاقد باشد")]
        [Compare(nameof(NewPassword), ErrorMessage = "رمزعبور جدید با تکرارش متفاوت است")]
        public string ConfirmNewPassword { get; set; }
    }
}
