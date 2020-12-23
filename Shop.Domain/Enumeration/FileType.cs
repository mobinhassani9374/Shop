using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Shop.Domain.Enumeration
{
    public enum FileType
    {
        [DisplayName("عکس فایل")]
        ProductImage = 1,

        [DisplayName("عکس اسلایدشو")]
        SlideShowImage = 2
    }
}
