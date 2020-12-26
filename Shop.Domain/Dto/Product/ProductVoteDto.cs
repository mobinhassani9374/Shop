using Shop.Domain.Dto.User;
using Shop.Domain.Enumeration;
using System;
using System.Collections.Generic;
using System.Text;

namespace Shop.Domain.Dto.Product
{
    public class ProductVoteDto
    {
        public int Id { get; set; }
        public string Comment { get; set; }
        public string UserId { get; set; }
        public UserDto User { get; set; }
        public DateTime Date { get; set; }
        public VoteState Stats { get; set; }
        public int ProductId { get; set; }
        public ProductDto Product { get; set; }
    }
}
