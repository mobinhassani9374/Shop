using Shop.Domain.Dto.User;
using System;
using System.Collections.Generic;
using System.Text;

namespace Shop.Domain.Dto.Order
{
    public class OrderDto
    {
        public int Id { get; set; }
        public long TotalPrice { get; set; }
        public DateTime Date { get; set; }
        public string UserId { get; set; }
        public bool IsFinal { get; set; }
        public string Description { get; set; }
        public bool IsPaid { get; set; }
        public DateTime? PaymentDate { get; set; }
        public List<OrderDetailDto> Details { get; set; }
        public UserDto User { get; set; }
    }
}
