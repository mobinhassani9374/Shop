using Shop.Database;
using System;
using System.Collections.Generic;
using System.Text;

namespace Shop.Services
{
    public abstract class BaseService
    {
        protected AppDbContext _dbContext;
        public BaseService(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }
    }
}
