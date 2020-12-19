using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Shop.Mvc.Models.Account
{
    public class RegisterViewModel
    {
        public string PhoneNumber { get; set; }
        public string FullName { get; set; }
        public string Password { get; set; }
        public string ConfirmPasswprd { get; set; }
    }
}
