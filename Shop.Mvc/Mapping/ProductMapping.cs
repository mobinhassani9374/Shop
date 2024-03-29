﻿using Shop.Domain.Dto.Pagination;
using Shop.Domain.Dto.Product;
using Shop.Mvc.Models.Pagination;
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
                ImageFile = source.ImageFile,
                IsAmazing = source.IsAmazing,
                Discount = source.Discount,
                Attributes = source.Attributes,
                Garanty = source.Garanty,
                GarantyCondition = source.GarantyCondition
            };
        }

        public static ProductSearchDto ToDto(this ProductSearchViewModel source)
        {
            return new ProductSearchDto
            {
                PageNumber = source.PageNumber,
                PageSize = source.PageSize,
                Title = source.Title
            };
        }

        public static PaginationViewModel<ProductViewModel> ToViewModel(this PaginationDto<ProductDto> source)
        {
            return new PaginationViewModel<ProductViewModel>
            {
                PageSize = source.PageSize,
                Count = source.Count,
                PageCount = source.PageCount,
                PageNumber = source.PageNumber,
                Data = source.Data.ToViewModel()
            };
        }

        public static List<ProductViewModel> ToViewModel(this List<ProductDto> sources)
        {
            var result = new List<ProductViewModel>();
            foreach (var source in sources)
                result.Add(source.ToViewModel());
            return result;
        }

        public static ProductViewModel ToViewModel(this ProductDto source)
        {
            return new ProductViewModel
            {
                Title = source.Title,
                Count = source.Count,
                Id = source.Id,
                Price = source.Price,
                PrimaryImage = source.PrimaryImage,
                Description = source.Description,
                Category = source.Category?.ToViewModel(),
                CategoryId = source.CategoryId,
                MoreImages = source.MoreImages,
                Discount = source.Discount,
                IsAmazing = source.IsAmazing,
                Garanty = source.Garanty,
                Attributes = source.Attributes,
                GarantyCondition = source.GarantyCondition
            };
        }

        public static UpdateProductDto ToDto(this UpdateProductViewModel source)
        {
            return new UpdateProductDto
            {
                CategoryId = source.CategoryId,
                Count = source.Count,
                Description = source.Description,
                Id = source.Id,
                ImageFile = source.ImageFile,
                Price = source.Price,
                Title = source.Title,
                Discount = source.Discount,
                IsAmazing = source.IsAmazing,
                Attributes = source.Attributes,
                Garanty = source.Garanty,
                GarantyCondition = source.GarantyCondition
            };
        }

        public static AddProductCommentDto ToDto(this AddCommentViewModel source)
        {
            return new AddProductCommentDto
            {
                Comment = source.Comment,
                ProductId = source.ProductId
            };
        }
        public static List<ProductVoteViewModel> ToViewModel(this List<ProductVoteDto> sources)
        {
            var result = new List<ProductVoteViewModel>();
            foreach (var source in sources)
                result.Add(source.ToViewModel());
            return result;
        }
        public static ProductVoteViewModel ToViewModel(this ProductVoteDto source)
        {
            return new ProductVoteViewModel
            {
                Comment = source.Comment,
                Date = source.Date,
                Id = source.Id,
                Product = source.Product?.ToViewModel(),
                ProductId = source.ProductId,
                Stats = source.Stats,
                User = source.User?.ToViewModel(),
                UserId = source.UserId
            };
        }
        public static ProductUserSearchDto ToDto(this ProductUserSearchViewModel source)
        {
            return new ProductUserSearchDto
            {
                CategoryId = source.CategoryId,
                PageNumber = source.PageNumber,
                PageSize = source.PageSize,
                Title = source.Title
            };
        }
    }
}
