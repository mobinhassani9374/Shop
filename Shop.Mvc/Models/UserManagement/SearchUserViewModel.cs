using Shop.Mvc.Attributes;
using Shop.Mvc.Enums;

namespace Shop.Mvc.Models.UserManagement
{
    public class SearchUserViewModel : PagingViewModel
    {
        public string FullName { get; set; }

        public string PhoneNumber { get; set; }

        [SelectItem()]
        public UserType Type { get; set; } = UserType.All;
    }
}
