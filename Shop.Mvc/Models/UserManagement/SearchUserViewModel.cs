using Shop.Domain.Enumeration;
using Shop.Mvc.Attributes;

namespace Shop.Mvc.Models.UserManagement
{
    public class SearchUserViewModel : PagingViewModel
    {
        public string FullName { get; set; }

        [SelectItem(typeof(UserType))]
        public UserType? Type { get; set; }
    }
}
