using Shop.Domain.Dto.Home;
using Shop.Mvc.Models.Home;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Shop.Mvc.Mapping
{
    public static class HomeMapping
    {
        public static UserIndexViewModel ToViewModel(this UserIndexDto source)
        {
            return new UserIndexViewModel
            {
                ProductCategries = source.ProductCategries.ToViewModel(),
                SlideShows = source.SlideShows.ToViewModel()
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
    }
}
