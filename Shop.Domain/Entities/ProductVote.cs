using Shop.Domain.Enumeration;
using System;
using System.Collections.Generic;
using System.Text;

namespace Shop.Domain.Entities
{
    public class ProductVote
    {
        public int Id { get; set; }
        public string Comment { get; set; }
        public string UserId { get; set; }
        public DateTime Date { get; set; }
        public VoteState Stats { get; set; }
        public int ProductId { get; set; }
        public Product Product { get; set; }
    }
}
