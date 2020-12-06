using System;
using System.Collections.Generic;
using System.Text;

namespace Shop.Domain.Dto.User
{
    public class SearchUserDto : PagingDto
    {
        public string FullName { get; set; }
        public bool IsManager { get; set; }
        public bool IsManual { get; set; }
    }
}
