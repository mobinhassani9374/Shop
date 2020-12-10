using Shop.Domain.Dto.Product;
using Shop.Mvc.Models.Product;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Shop.Mvc.Mapping
{
    public static class ProductMapping
    {
        public static CreateProductDto ToDto(this CreateProductViewModel source)
        {
            return new CreateProductDto
            {
                CategoryId = source.CategoryId,
                Count = source.Count,
                Description = source.Description,
                Price = source.Price,
                Title = source.Title,
                ImageFile = source.ImageFile
            };
        }
    }
}
