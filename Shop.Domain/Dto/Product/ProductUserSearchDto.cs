using System;
using System.Collections.Generic;
using System.Text;

namespace Shop.Domain.Dto.Product
{
    public class ProductUserSearchDto : PagingDto
    {
        public string Title { get; set; }
        public int CategoryId { get; set; }
    }
}
