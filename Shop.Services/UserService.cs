using Microsoft.AspNetCore.Hosting;
using Shop.Database;
using System;
using System.Collections.Generic;
using System.Text;

namespace Shop.Services
{
    public class UserService : BaseService
    {
        public UserService(AppDbContext dbContext,
            IHostingEnvironment env) : base(dbContext, env)
        {

        }
    }
}
