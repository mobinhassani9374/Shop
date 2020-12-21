using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Shop.Database;
using Shop.Database.Identity.Entities;
using Shop.Domain.Dto.Account;
using Shop.Domain.Dto.Cart;
using Shop.Domain.Dto.Home;
using Shop.Domain.Enumeration;
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
                    user.Type = UserType.Manual;
                    var identityResult = await _userManager.CreateAsync(user, dto.Password);
                    if (identityResult.Succeeded)
                        serviceResult.Message = "یک کاربر با موفقیت ثبت نام شد";
                    else
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
        public ServiceResult AddToCart(AddToCartDto dto)
        {
            var serviceResult = new ServiceResult(true);

            var product = _dbContext.Products.FirstOrDefault(c => c.Id == dto.ProductId);

            if (product == null)
                serviceResult.AddError("محصولی یافت نشد");
            else
            {
                if (product.Count == 0)
                    serviceResult.AddError("کالا ناموجود است");
                else
                {
                    if (!_dbContext.Users.Any(c => c.Id == dto.UserId))
                        serviceResult.AddError("کاربری یافت نشد");
                    else
                    {
                        var entity = dto.ToEntity();
                        entity.Date = DateTime.Now;
                        Insert(entity);
                        serviceResult = Save("یک کالا با موفقیت به سبد خرید اضافه شد");
                    }
                }
            }


            return serviceResult;
        }
        public List<CartDto> GetCarts(string userId)
        {
            var carts = _dbContext
                 .Carts
                 .Include(c => c.Product)
                 .Where(c => c.UserId == userId)
                 .ToList();

            var result = new List<CartDto>();

            var groupingCart = carts.GroupBy(c => c.ProductId).ToList();

            groupingCart.ForEach(c =>
            {
                result.Add(new CartDto
                {
                    ProductId = c.Key,
                    Count = c.Count(),
                    Price = c.First().Product.Price,
                    ProductTitle = c.First().Product.Title,
                    ProductImage = c.First().Product.PrimaryImage
                });
            });

            return result;
        }
    }
}
