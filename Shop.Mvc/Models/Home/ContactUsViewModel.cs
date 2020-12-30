using Shop.Mvc.Models.UserManagement;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Shop.Mvc.Models.Home
{
    public class ContactUSViewModel
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public string UserId { get; set; }
        public string Date { get; set; }
        public UserViewModel User { get; set; }
    }
}
