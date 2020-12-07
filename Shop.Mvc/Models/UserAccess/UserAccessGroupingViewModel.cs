using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Shop.Mvc.Models.UserAccess
{
    public class UserAccessGroupingViewModel
    {
        public string Title { get; set; }
        public List<UserAccessItemViewModel> Items { get; set; } = new List<UserAccessItemViewModel>();
    }
}
