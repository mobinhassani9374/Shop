﻿using Shop.Domain.Dto.Order;
using Shop.Mvc.Models.Order;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DNTPersianUtils.Core;

namespace Shop.Mvc.Mapping
{
    public static class OrderMapping
    {
        public static List<OrderViewModel> ToViewModel(this List<OrderDto> sources)
        {
            var result = new List<OrderViewModel>();
            foreach (var source in sources)
                result.Add(source.ToViewModel());
            return result;
        }

        public static OrderViewModel ToViewModel(this OrderDto source)
        {
            return new OrderViewModel
            {
                UserId = source.UserId,
                Date = source.Date.ToFriendlyPersianDateTextify(),
                Description = source.Description,
                Id = source.Id,
                IsFinal = source.IsFinal,
                IsPaid = source.IsPaid,
                PaymentDate = source.PaymentDate,
                TotalPrice = source.TotalPrice,
                Details = source.Details?.ToViewModel()
            };
        }
        public static List<OrderDetailViewModel> ToViewModel(this List<OrderDetailDto> sources)
        {
            var result = new List<OrderDetailViewModel>();
            foreach (var source in sources)
                result.Add(source.ToViewModel());
            return result;
        }
        public static OrderDetailViewModel ToViewModel(this OrderDetailDto source)
        {
            return new OrderDetailViewModel
            {
                Count = source.Count,
                Id = source.Id,
                OrderId = source.OrderId,
                Price = source.Price,
                Product = source.Product?.ToViewModel(),
                ProductId = source.ProductId
            };
        }
    }
}