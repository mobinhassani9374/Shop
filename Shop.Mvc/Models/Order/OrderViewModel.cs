﻿using Shop.Mvc.Models.UserManagement;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Shop.Mvc.Models.Order
{
    public class OrderViewModel
    {
        public int Id { get; set; }
        public long TotalPrice { get; set; }
        public string Date { get; set; }
        public string UserId { get; set; }
        public bool IsFinal { get; set; }
        public string Description { get; set; }
        public bool IsPaid { get; set; }
        public DateTime? PaymentDate { get; set; }
        public long ShippingPrice { get; set; }
        public string Address { get; set; }
        public List<OrderDetailViewModel> Details { get; set; }
        public UserViewModel User { get; set; }
    }
}
