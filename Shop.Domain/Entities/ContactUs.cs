using System;
using System.Collections.Generic;
using System.Text;

namespace Shop.Domain.Entities
{
    public class ContactUs
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public string UserId { get; set; }
        public DateTime Date { get; set; }
    }
}
