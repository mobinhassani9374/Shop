using System;
using System.Collections.Generic;
using System.Text;

namespace Shop.Domain.Dto
{
    public abstract class PagingDto
    {
        public int PageSize { get; set; }
        public int PageNumber { get; set; }
    }
}
