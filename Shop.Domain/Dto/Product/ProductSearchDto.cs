﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Shop.Domain.Dto.Product
{
    public class ProductSearchDto : PagingDto
    {
        public string Title { get; set; }
    }
}
