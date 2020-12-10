using Shop.Domain.Dto.Product;
using Shop.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Shop.Services.Mapping
{
    public static class ProductMapping
    {
        public static Product ToEntity(this CreateProductDto source)
        {
            return new Product
            {
                CategoryId = source.CategoryId,
                Count = source.Count,
                Description = source.Description,
                Price = source.Price,
                PrimaryImage = source.ImageName,
                Title = source.Title
            };
        }
    }
}
