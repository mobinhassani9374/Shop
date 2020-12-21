using Shop.Domain.Dto.Cart;
using Shop.Mvc.Models.Cart;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Shop.Mvc.Mapping
{
    public static class CartMapping
    {
        public static AddToCartDto ToDto(this AddToCartViewModel source, string userId)
        {
            return new AddToCartDto
            {
                ProductId = source.ProductId,
                UserId = userId
            };
        }

        public static List<CartViewModel> ToViewModel(this List<CartDto> sources)
        {
            var result = new List<CartViewModel>();
            foreach (var source in sources)
                result.Add(source.ToViewModel());
            return result;
        }

        public static CartViewModel ToViewModel(this CartDto source)
        {
            return new CartViewModel
            {
                Count = source.Count,
                Price = source.Price,
                ProductId = source.ProductId,
                ProductTitle = source.ProductTitle,
                ProductImage = source.ProductImage,
                ProductCount = source.ProductCount
            };
        }
    }
}
