using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Shop.Domain.Dto.Representations
{
    public class RepresentationCreateDto
    {
        [Required(ErrorMessage = "عنوان نمی تواند فاقد مقدار باشد")]
        [MaxLength(Constants.Representation_Title_Length, ErrorMessage = "تعداد کاراکترهای عنوان نمی تواند بیش از @ کاراکتر باشد")]
        public string Title { get; set; }

        [Required(ErrorMessage = "آدرس نمی تواند فاقد مقدار باشد")]
        [MaxLength(Constants.Address_Length, ErrorMessage = "تعداد کاراکترهای آدرس نمی تواند بیش از @ کاراکتر باشد")]
        public string Address { get; set; }

        [Required(ErrorMessage = "نام و نام خانوادگی نمی تواند فاقد مقدار باشد")]
        [MaxLength(Constants.User_FullName_Length, ErrorMessage = "تعداد کاراکترهای نام و نام خانوادگی نمی تواند بیش از @ کاراکتر باشد")]
        public string FullName { get; set; }

        [Required(ErrorMessage = "شماره همراه نمی تواند فاقد مقدار باشد")]
        [MaxLength(Constants.PhoneNumber_Length, ErrorMessage = "تعداد کاراکترهای شماره تماس نمی تواند بیش از @ کاراکتر باشد")]
        public string PhoneNumber { get; set; }

        [MaxLength(Constants.InstagramAccount_Length, ErrorMessage = "تعداد کاراکترهای اکانت اینستاگرام نمی تواند بیش از @ کاراکتر باشد")]
        public string InstagramAccount { get; set; }

        [MaxLength(Constants.PhoneNumber_Length, ErrorMessage = "تعداد کاراکترهای شماره همراه واتساپ نمی تواند بیش از @ کاراکتر باشد")]
        public string WhatsAppNumber { get; set; }
    }
}
