using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Shop.Database;
using Shop.Database.Identity.Entities;
using Shop.Domain.Dto.Account;
using Shop.Domain.Dto.Cart;
using Shop.Domain.Dto.Home;
using Shop.Domain.Dto.Order;
using Shop.Domain.Dto.Product;
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
                Products = c.Products.Select(i => new Domain.Dto.Home.ProductDto
                {
                    Id = i.Id,
                    Title = i.Title,
                    Count = i.Count,
                    Price = i.Price,
                    PrimaryImage = i.PrimaryImage
                }).ToList()
            }).ToList();

            result.SlideShows = GetAllSlideShows();

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
                    ProductImage = c.First().Product.PrimaryImage,
                    ProductCount = c.First().Product.Count
                });
            });

            return result;
        }
        public ServiceResult ReduceFromCart(ReduceDto dto)
        {
            var serviceResult = new ServiceResult(true);

            var entity = _dbContext.Carts.LastOrDefault(c => c.ProductId == dto.ProductId && c.UserId == dto.UserId);

            if (entity == null)
                serviceResult.AddError("محصولی با این شناسه برای شما در سبد خریدتون وجود ندارد");
            else
            {
                Remove(entity);
                serviceResult = Save("عملیات با موفقیت صورت گرفت");
            }


            return serviceResult;
        }
        public ServiceResult DeleteFromCart(int productId, string userId)
        {
            var carts = _dbContext.Carts.Where(c => c.ProductId == productId && c.UserId == userId).ToList();

            carts.ForEach(c =>
            {
                Remove(c);
            });

            return Save("عملیات با موفقیت صورت گرفت");
        }
        public ServiceResult<int> ConvertCartToOrder(string userId)
        {
            var serviceResult = new ServiceResult<int>(true);

            var carts = _dbContext
                 .Carts
                 .Include(c => c.Product)
                 .Where(c => c.UserId == userId)
                 .ToList();

            var orderEntity = carts.ToEntity();
            orderEntity.Date = DateTime.Now;
            orderEntity.UserId = userId;
            Insert(orderEntity);
            var saveResult = Save("عملیات با موفقیت صورت گرفت");
            if (saveResult.IsSuccess)
            {
                foreach (var detail in orderEntity.Details)
                    detail.OrderId = orderEntity.Id;

                Insert(orderEntity.Details.ToList());
                Remove(carts);
                Save("عملیات با موفقیت صورت گرفت");

                serviceResult.Data = orderEntity.Id;
            }

            else serviceResult.AddError(saveResult.Errors.FirstOrDefault());
            return serviceResult;
        }
        public ServiceResult SuccessPayment(int orderId)
        {
            var serviceResult = new ServiceResult(true);

            var order = _dbContext
                .Orders
                .Include(c => c.Details)
                .ThenInclude(c => c.Product)
                .FirstOrDefault(c => c.Id == orderId);

            if (order == null)
                serviceResult.AddError("شناسه نا معتبر");
            else
            {
                order.IsPaid = true;
                order.PaymentDate = DateTime.Now;

                foreach (var detail in order.Details)
                {
                    detail.Product.Count -= detail.Count;
                    Update(detail.Product);
                }

                Update(order);
                serviceResult = Save("عملیات با موفقیت انجام شد");
            }
            return serviceResult;
        }
        public List<OrderDto> GetMyOrders(string userId)
        {
            var data = _dbContext.Orders
                .Include(c => c.Details)
                .ThenInclude(c => c.Product)
                .OrderByDescending(c => c.Id)
                .Where(c => c.UserId == userId)
                .ToList();

            return data.ToDto();
        }
        public ServiceResult AddCommentForProduct(AddProductCommentDto dto, string userId)
        {
            var serviceResult = dto.IsValid();
            if (serviceResult.IsSuccess)
            {
                if (_dbContext.Products.Any(c => c.Id == dto.ProductId))
                {
                    var entity = dto.ToEntity(userId);
                    entity.Date = DateTime.Now;
                    entity.Stats = VoteState.Wating;

                    Insert(entity);
                    serviceResult = Save("نظر با موفقیت ثبت شد");
                }
                else serviceResult.AddError("محصولی یافت نشد");
            }
            return serviceResult;
        }
        public List<ProductVoteDto> GetAllAcepteCommentForProduct(int id)
        {
            var data = _dbContext.ProductVote
                .Where(c => c.Stats == VoteState.Accepted && c.ProductId == id)
                .ToList();

            var result = data.ToDto();

            var users = GetUsers(result.Select(c => c.UserId).ToList());

            SetUsers(result, users);

            return result;
        }
        public ServiceResult CreateContactUs(CreateContactUsDto dto)
        {
            var serviceResult = dto.IsValid();
            if (serviceResult.IsSuccess)
            {
                var entity = dto.ToEntity();
                entity.Date = DateTime.Now;
                Insert(entity);
                serviceResult = Save("عملیات با موفقیت صورت گرفت");
            }
            return serviceResult;
        }
    }
}
