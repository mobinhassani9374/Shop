using Shop.Domain.Dto;
using Shop.Utility;
using System;
using System.Collections.Generic;
using System.Text;
using Shop.Utility.Extensions;
using DNTPersianUtils.Core;

namespace Shop.Services.Validations
{
    public static class UserValidation
    {
        public static ServiceResult IsValid(this CreateUserDto dto)
        {
            var servieResult = new ServiceResult(true);

            if (dto.FullName.IsNullOrEmpty())
                servieResult.AddError("نام و نام خانوادگی نمی تواند فاقد مقدار باشد");

            if (dto.PhoneNumber.IsNullOrEmpty())
                servieResult.AddError("شماره همراه نمی تواند فاقد مقدار باشد");

            if (!dto.PhoneNumber.IsNullOrEmpty() && !dto.PhoneNumber.IsValidIranianMobileNumber())
                servieResult.AddError("ساختار شماره همراه وارد شده معتبر نمی باشد");

            if (dto.Password.IsNullOrEmpty())
                servieResult.AddError("رمزعبور نمی تواند فاقد باشد");

            if (dto.ConfirmPassword.IsNullOrEmpty())
                servieResult.AddError("تکرار رمزعبور نمی تواند فاقد باشد");

            if (!dto.ConfirmPassword.IsNullOrEmpty() && !dto.Password.IsNullOrEmpty())
            {
                if (dto.ConfirmPassword != dto.Password)
                    servieResult.AddError("رمزعبور و تکرارش باهم یکسان نیستند");
            }

            return servieResult;
        }
    }
}
