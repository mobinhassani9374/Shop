﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Shop.Domain.Dto.Cart
{
    public class AddToCartDto
    {
        public int ProductId { get; set; }
        public string UserId { get; set; }
    }
}
