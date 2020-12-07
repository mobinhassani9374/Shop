using System;
using System.Collections.Generic;
using System.Text;

namespace Shop.Domain.Dto.UserAccess
{
    public class UserAccessGroupingDto
    {
        public string Title { get; set; }
        public List<UserAccessItemDto> Items { get; set; } = new List<UserAccessItemDto>();
    }
}
