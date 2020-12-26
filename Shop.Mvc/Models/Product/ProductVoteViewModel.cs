using Shop.Domain.Enumeration;
using Shop.Mvc.Models.UserManagement;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Shop.Mvc.Models.Product
{
    public class ProductVoteViewModel
    {
        public int Id { get; set; }
        public string Comment { get; set; }
        public string UserId { get; set; }
        public UserViewModel User { get; set; }
        public DateTime Date { get; set; }
        public VoteState Stats { get; set; }
        public int ProductId { get; set; }
        public ProductViewModel Product { get; set; }
    }
}
