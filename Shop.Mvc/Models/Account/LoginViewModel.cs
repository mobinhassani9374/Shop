using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Shop.Mvc.Models.Account
{
    public class LoginViewModel
    {
        public string PhoneNumber { get; set; }
        public string Password { get; set; }
        public string ReturnUrl { get; set; }
    }
}
