﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Shop.Domain.Entities
{
    public class Order
    {
        public int Id { get; set; }
        public long TotalPrice { get; set; }
        public DateTime Date { get; set; }
        public string UserId { get; set; }
        public bool IsFinal { get; set; }
        public string Description { get; set; }
        public bool IsPaid { get; set; }
        public string IdPay_id { get; set; }
        public DateTime? PaymentDate { get; set; }
        public long ShippingPrice { get; set; }
        public string Address { get; set; }
        public ICollection<OrderDetail> Details { get; set; }
    }
}
