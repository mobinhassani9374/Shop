using Shop.Utility;
using Shop.Utility.Extensions;
using DNTPersianUtils.Core;
using Shop.Domain.Dto.User;

namespace Shop.Services.Validations
{
    public static class UserValidation
    {
        public static ServiceResult IsValid(this CreateUserDto dto)
        {
            var servieResult = new ServiceResult(true);

            if (dto.FullName.IsNullOrEmpty())
                servieResult.AddError("نام و نام خانوادگی نمی تواند فاقد مقدار باشد");

            if (!dto.FullName.IsNullOrEmpty() && dto.FullName.Length > Domain.Constants.User_FullName_Length)
                servieResult.AddError($"تعداد کاراکترهای نام و نام خانوادگی نمی تواند بیش از {Domain.Constants.User_FullName_Length.ToPersianNumbers()} باشد");

            if (dto.PhoneNumber.IsNullOrEmpty())
                servieResult.AddError("شماره همراه نمی تواند فاقد مقدار باشد");

            if (!dto.PhoneNumber.IsNullOrEmpty() && !dto.PhoneNumber.IsValidIranianMobileNumber())
                servieResult.AddError("ساختار شماره همراه وارد شده معتبر نمی باشد");

            if (dto.Password.IsNullOrEmpty())
                servieResult.AddError("رمزعبور نمی تواند فاقد باشد");

            if (!dto.Password.IsNullOrEmpty() && dto.Password.Length < Domain.Constants.Password_Length)
                servieResult.AddError($"رمزعبور نباید کمتر از {Domain.Constants.Password_Length.ToPersianNumbers()} کاراکتر باشد");

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
