using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Shop.Domain.Dto.Account
{
    public class RegisterDto
    {
        [Required(ErrorMessage = "شماره همراه نمی تواند فاقد باشد")]
        [Phone(ErrorMessage = "ساختار شماره همراه وارد شده صحیح نمی باشد")]
        public string PhoneNumber { get; set; }

        [Required(ErrorMessage = "نام و نام خانوادگی نمی تواند فاقد باشد")]
        public string FullName { get; set; }

        [Required(ErrorMessage = "رمزعبور نمی تواند فاقد باشد")]
        [MinLength(Constants.Password_Length, ErrorMessage = "رمزعبور نمی تواند کمتر از @ کاراکتر باشد")]
        public string Password { get; set; }

        [Required(ErrorMessage = "تکرار رمز عبور نمی تواند فاقد باشد")]
        [Compare(nameof(Password), ErrorMessage = "رمزعبور با تکرارش متفاوت است")]
        public string ConfirmPasswprd { get; set; }
    }
}
