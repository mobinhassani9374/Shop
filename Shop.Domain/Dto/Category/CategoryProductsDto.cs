using Shop.Domain.Dto.Pagination;
using System;
using System.Collections.Generic;
using System.Text;

namespace Shop.Domain.Dto.Category
{
    public class CategoryProductsDto
    {
        public PaginationDto<Domain.Dto.Product.ProductDto> Products { get; set; }
        public string CategoryName { get; set; }
    }
}
