using Shop.Database;
using System;
using System.Collections.Generic;
using System.Text;

namespace Shop.Services
{
    public class UserService : BaseService
    {
        public UserService(AppDbContext dbContext) : base(dbContext)
        {

        }
    }
}
