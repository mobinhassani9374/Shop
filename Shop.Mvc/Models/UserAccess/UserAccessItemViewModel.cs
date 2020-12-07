using Shop.Domain.Enumeration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Shop.Mvc.Models.UserAccess
{
    public class UserAccessItemViewModel
    {
        public string Title { get; set; }
        public int Id { get { return (int)Code; } }
        public AccessCode Code { get; set; }
        public bool Checked { get; set; }
    }
}
