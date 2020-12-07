using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace Shop.Domain.Enumeration
{
    public enum AccessCode
    {
        // دسترسی کامل
        FullAccess = 0,

        // مدیریت کاربران
        UserManagement = 100,
        CreateUser = 101,
        DeleteUser = 102,

        [DisplayName("مشاهده کاربران")]
        ViewUser = 103,
        EditUser = 104,
        AccessManagement = 105,
    }
}
