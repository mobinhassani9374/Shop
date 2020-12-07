using Shop.Domain.Enumeration;
using System;
using System.Collections.Generic;
using System.Text;

namespace Shop.Domain.Dto.User
{
    public class SearchUserDto : PagingDto
    {
        public string FullName { get; set; }
        public string PhoneNumber { get; set; }
        public UserType? Type { get; set; }
    }
}
