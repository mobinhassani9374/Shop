using Shop.Domain.Enumeration;
using System;
using System.Collections.Generic;
using System.Text;

namespace Shop.Domain.Dto.UserAccess
{
    public class UserAccessItemDto
    {
        public string Title { get; set; }
        public int Id { get { return (int)Code; } }
        public AccessCode Code { get; set; }
        public bool Checked { get; set; }
    }
}
