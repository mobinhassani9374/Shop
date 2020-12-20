using Shop.Domain.Dto.Cart;
using Shop.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Shop.Services.Mapping
{
    public static class CartMapping
    {
        public static Cart ToEntity(this AddToCartDto source)
        {
            return new Cart
            {
                ProductId = source.ProductId,
                UserId = source.UserId
            };
        }
        public static List<CartDto> ToDto(this List<Cart> sources)
        {
            var result = new List<CartDto>();
            foreach (var source in sources)
                result.Add(source.ToDto());
            return result;
        }
        public static CartDto ToDto(this Cart source)
        {
            return new CartDto
            {
                Count = 1,
                Price = source.Product.Price,
                ProductId = source.ProductId,
                ProductTitle = source.Product.Title
            };
        }
    }
}
