using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Shop.Database;
using Shop.Domain.Dto.Home;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Shop.Services
{
    public class UserService : BaseService
    {
        public UserService(AppDbContext dbContext,
            IHostingEnvironment env) : base(dbContext, env)
        {

        }
        public UserIndexDto GetUserIndexData()
        {
            var result = new UserIndexDto();

            var categories = _dbContext.Categories
                 .Include(c => c.Children)
                 .Include(c => c.Products)
                 .Where(c => !c.ParentId.HasValue)
                 .ToList();

            result.ProductCategries = categories.Select(c => new CategoryDto
            {
                CategoryTitle = c.Title,
                Id = c.Id,
                Products = c.Products.Select(i => new ProductDto
                {
                    Id = i.Id,
                    Title = i.Title,
                    Count = i.Count,
                    Price = i.Price,
                    PrimaryImage = i.PrimaryImage
                }).ToList()
            }).ToList();

            return result;
        }
    }
}
