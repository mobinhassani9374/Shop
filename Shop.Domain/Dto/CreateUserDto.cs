using System;
using System.Collections.Generic;
using System.Text;

namespace Shop.Domain.Dto
{
    public class CreateUserDto
    {
        public string FullName { get; set; }
        public string PhoneNumber { get; set; }
        public string Password { get; set; }
        public string ConfirmPassword { get; set; }
        public bool IsManager { get; set; }
    }
}
