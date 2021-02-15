using System;
using System.Collections.Generic;
using System.Text;

namespace Shop.Domain.Dto.Representations
{
    public class RepresentationDto
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
