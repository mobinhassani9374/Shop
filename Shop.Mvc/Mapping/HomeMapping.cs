using Shop.Domain.Dto.Home;
using Shop.Mvc.Models.Home;
using Shop.Mvc.Models.Pagination;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DNTPersianUtils.Core;

namespace Shop.Mvc.Mapping
{
    public static class HomeMapping
    {
        public static UserIndexViewModel ToViewModel(this UserIndexDto source)
        {
            return new UserIndexViewModel
            {
                ProductCategries = source.ProductCategries.ToViewModel(),
                SlideShows = source.SlideShows.ToViewModel(),
                ProductsAmazing = source.ProductsAmazing.ToViewModel()
            };
        }
        public static List<CategoryViewModel> ToViewModel(this List<CategoryDto> sources)
        {
            var result = new List<CategoryViewModel>();
            foreach (var source in sources)
                result.Add(source.ToViewModel());
            return result;
        }
        public static CategoryViewModel ToViewModel(this CategoryDto source)
        {
            return new CategoryViewModel
            {
                CategoryTitle = source.CategoryTitle,
                Id = source.Id,
                Products = source.Products.ToViewModel()
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
                Id = source.Id,
                Count = source.Count,
                Price = source.Price,
                PrimaryImage = source.PrimaryImage,
                Title = source.Title
            };
        }
        public static CreateContactUsDto ToDto(this CreateContactUsViewModel source, string userId)
        {
            return new CreateContactUsDto
            {
                UserId = userId,
                Description = source.Description
            };
        }
        public static ContactUsSearchDto ToDto(this ContactUSSearchViewModel source)
        {
            return new ContactUsSearchDto
            {
                PageNumber = source.PageNumber,
                PageSize = source.PageSize
            };
        }

        public static PaginationViewModel<ContactUSViewModel> ToViewModel(this Domain.Dto.Pagination.PaginationDto<ContactUsDto> source)
        {
            return new PaginationViewModel<ContactUSViewModel>()
            {
                Count = source.Count,
                Data = source.Data.ToViewModel(),
                PageCount = source.PageCount,
                PageNumber = source.PageNumber,
                PageSize = source.PageSize
            };
        }
        public static List<ContactUSViewModel> ToViewModel(this List<ContactUsDto> sources)
        {
            var result = new List<ContactUSViewModel>();
            foreach (var source in sources)
                result.Add(source.ToViewModel());
            return result;
        }
        public static ContactUSViewModel ToViewModel(this ContactUsDto source)
        {
            return new ContactUSViewModel
            {
                Description = source.Description,
                UserId = source.UserId,
                Id = source.Id,
                Date = source.Date.ToFriendlyPersianDateTextify(),
                User = source.User?.ToViewModel()
            };
        }
    }
}
