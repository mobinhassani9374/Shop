using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

namespace Shop.Mvc.Enums
{
    public enum UserType
    {
        [DisplayName("همه موارد")]
        All = 0,

        [DisplayName("ادمین")]
        Admin = 2,

        [DisplayName("کاربر عادی")]
        Manual = 3
    }
}
