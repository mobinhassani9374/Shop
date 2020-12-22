using Shop.Domain.Dto.Order;
using Shop.Mvc.Models.Order;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

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
                Date = source.Date,
                Description = source.Description,
                Id = source.Id,
                IsFinal = source.IsFinal,
                IsPaid = source.IsPaid,
                PaymentDate = source.PaymentDate,
                TotalPrice = source.TotalPrice
            };
        }
    }
}
