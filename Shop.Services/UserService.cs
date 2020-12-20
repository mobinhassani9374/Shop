using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Shop.Database;
using Shop.Database.Identity.Entities;
using Shop.Domain.Dto.Account;
using Shop.Domain.Dto.Home;
using Shop.Services.Mapping;
using Shop.Services.Validations;
using Shop.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Services
{
    public class UserService : BaseService
    {
        private readonly UserManager<User> _userManager;
        public UserService(AppDbContext dbContext,
            IHostingEnvironment env,
            UserManager<User> userManager) : base(dbContext, env)
        {
            _userManager = userManager;
        }
        public async Task<ServiceResult> Register(RegisterDto dto)
        {
            var serviceResult = dto.IsValid();
            if (serviceResult.IsSuccess)
            {
                if (ExistUserByPhone(dto.PhoneNumber))
                    serviceResult.AddError("شماره همراه وارد قبلا ثبت نام کرده است");
                else
                {
                    var user = dto.ToEntity();
                    user.RegisterDate = DateTime.Now;
                    var identityResult = await _userManager.CreateAsync(user, dto.Password);
                    if (!identityResult.Succeeded)
                    {
                        identityResult.Errors.ToList().ForEach(c =>
                        {
                            serviceResult.AddError(c.Code);
                        });
                    }
                }
            }
            return serviceResult;
        }
        public bool ExistUserByPhone(string phoneNumber)
        {
            return _dbContext.Users.Any(c => c.PhoneNumber == phoneNumber);
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
