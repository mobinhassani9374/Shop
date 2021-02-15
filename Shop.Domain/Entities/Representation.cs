﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Shop.Domain.Entities
{
    public class Representation
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Address { get; set; }
        public string FullName { get; set; }
        public string PhoneNumber { get; set; }
        public string InstagramAccount { get; set; }
        public string WhatsAppNumber { get; set; }
    }
}
