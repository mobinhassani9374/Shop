using Shop.Domain.Dto.User;
using System;
using System.Collections.Generic;
using System.Text;

namespace Shop.Domain.Dto.Home
{
    public class ContactUsDto
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public string UserId { get; set; }
        public DateTime Date { get; set; }
        public UserDto User { get; set; }
    }
}
