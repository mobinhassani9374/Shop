using Shop.Domain.Enumeration;
using System;

namespace Shop.Domain.Dto.User
{
    public class UserDto
    {
        public string Id { get; set; }
        public string FullName { get; set; }
        public string PhoneNumber { get; set; }
        public DateTime RegisterDate { get; set; }
        public UserType Type { get; set; }
    }
}
