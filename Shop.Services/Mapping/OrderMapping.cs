using Shop.Domain.Dto.Cart;
using Shop.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Shop.Services.Mapping
{
    public static class OrderMapping
    {
        public static Order ToEntity(this List<Cart> sources)
        {
            var dtos = new List<CartDto>();

            var order = new Order();
            order.TotalPrice = 0;
            order.Details = new List<OrderDetail>();

            var groupingCart = sources.GroupBy(c => c.ProductId).ToList();

            groupingCart.ForEach(c =>
            {
                dtos.Add(new CartDto
                {
                    ProductId = c.Key,
                    Count = c.Count(),
                    Price = c.First().Product.Price,
                    ProductTitle = c.First().Product.Title,
                    ProductImage = c.First().Product.PrimaryImage,
                    ProductCount = c.First().Product.Count
                });
            });

            foreach (var dto in dtos)
            {
                order.Details.Add(new OrderDetail
                {
                    Count = dto.Count,
                    Price = dto.Price,
                    ProductId = dto.ProductId
                });
                order.TotalPrice += (dto.Price * dto.Count);
            }
            return order;
        }
    }
}
