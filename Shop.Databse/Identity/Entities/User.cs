using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;

namespace Shop.Database.Identity.Entities
{
    public class User : IdentityUser
    {
        public string FullName { get; set; }
        public DateTime RegisterDate { get; set; }
        public bool IsProgrammer { get; set; }
    }
}
