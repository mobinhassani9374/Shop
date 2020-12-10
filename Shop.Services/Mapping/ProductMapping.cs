using Shop.Domain.Dto.Pagination;
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

        public static PaginationDto<ProductDto> ToDto(this PaginationDto<Product> source)
        {
            return new PaginationDto<ProductDto>()
            {
                Count = source.Count,
                Data = source.Data.ToDto(),
                PageCount = source.PageCount,
                PageNumber = source.PageNumber,
                PageSize = source.PageSize
            };
        }

        public static List<ProductDto> ToDto(this List<Product> sources)
        {
            var result = new List<ProductDto>();
            foreach (var source in sources)
                result.Add(source.ToDto());
            return result;
        }

        public static ProductDto ToDto(this Product source)
        {
            return new ProductDto
            {
                CategoryId = source.CategoryId,
                Category = source.Category?.ToDto(),
                Count = source.Count,
                Description = source.Description,
                Id = source.Id,
                Price = source.Price,
                PrimaryImage = source.PrimaryImage,
                Title = source.Title
            };
        }
    }
}
