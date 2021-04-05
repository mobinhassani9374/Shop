using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Shop.Database;
using Shop.Database.Identity.Entities;
using Shop.Domain.Dto.Account;
using Shop.Domain.Dto.Cart;
using Shop.Domain.Dto.Category;
using Shop.Domain.Dto.Home;
using Shop.Domain.Dto.Order;
using Shop.Domain.Dto.Pagination;
using Shop.Domain.Dto.Product;
using Shop.Domain.Dto.Representations;
using Shop.Domain.Dto.User;
using Shop.Domain.Entities;
using Shop.Domain.Enumeration;
using Shop.Services.Mapping;
using Shop.Services.Messaging.FarazSms;
using Shop.Services.Messaging.FarazSms.Models;
using Shop.Services.Validations;
using Shop.Utility;
using Shop.Utility.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Services
{
    public class UserService : BaseService
    {
        private readonly UserManager<User> _userManager;
        private readonly IHttpClientFactory _httpClientFactory;
        public UserService(AppDbContext dbContext,
            IHostingEnvironment env,
            UserManager<User> userManager,
             SmsService smsService,
             IHttpClientFactory httpClientFactory) : base(dbContext, env, smsService)
        {
            _userManager = userManager;
            _httpClientFactory = httpClientFactory;
        }
        List<Product> _product = new List<Product>();
        List<int> _catIds = new List<int>();
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
                 .AsEnumerable()
                 .Where(c => !c.ParentId.HasValue)
                 .ToList();

            result.ProductCategries = new List<Domain.Dto.Home.CategoryDto>();

            foreach (var category in categories)
            {
                var output = new Domain.Dto.Home.CategoryDto();
                output.Id = category.Id;
                output.CategoryTitle = category.Title;
                _product.AddRange(category.Products.ToList());
                GetProduct(category.Children.ToList());
                output.Products = _product.Select(i => new Domain.Dto.Home.ProductDto
                {
                    Id = i.Id,
                    Title = i.Title,
                    Count = i.Count,
                    Price = i.Price,
                    PrimaryImage = i.PrimaryImage
                }).ToList();
                _product.Clear();
                result.ProductCategries.Add(output);
            }

            result.SlideShows = GetAllSlideShows();

            result.ProductsAmazing = _dbContext
                .Products
                .Where(c => c.IsAmazing && c.Count > 0)
                .ToList()
                .ToDto();

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
                if (product.Count <= 0)
                    serviceResult.AddError("کالا ناموجود است");
                else
                {
                    if (!_dbContext.Users.Any(c => c.Id == dto.UserId))
                        serviceResult.AddError("کاربری یافت نشد");
                    else
                    {

                        var countCart = _dbContext.Carts.Count(c => c.UserId == dto.UserId && c.ProductId == dto.ProductId);

                        if (countCart >= product.Count)
                            serviceResult.AddError("کالا ناموجود است");

                        else
                        {
                            var entity = dto.ToEntity();
                            entity.Date = DateTime.Now;
                            Insert(entity);
                            serviceResult = Save("یک کالا با موفقیت به سبد خرید اضافه شد");
                        }
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
                    ProductCount = c.First().Product.Count,
                    DisCount = c.First().Product.IsAmazing ? c.First().Product.Discount : 0
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
        public ServiceResult<OrderDto> ConvertCartToOrder(string userId, string address)
        {
            var serviceResult = new ServiceResult<OrderDto>(true);

            var carts = _dbContext
                 .Carts
                 .Include(c => c.Product)
                 .Where(c => c.UserId == userId)
                 .ToList();

            var orderEntity = carts.ToEntity();
            orderEntity.Date = DateTime.Now;
            orderEntity.UserId = userId;
            orderEntity.Address = address;
            if (carts.Sum(c => c.Product.Price) < 700000)
            {
                orderEntity.ShippingPrice = 20000;
                orderEntity.TotalPrice += orderEntity.ShippingPrice;
            }

            Insert(orderEntity);
            var saveResult = Save("عملیات با موفقیت صورت گرفت");
            if (saveResult.IsSuccess)
            {
                var user = GetUser(userId);
                if (string.IsNullOrEmpty(user.Address))
                {
                    user.Address = address;
                    Update(user);
                }

                foreach (var detail in orderEntity.Details)
                    detail.OrderId = orderEntity.Id;

                Insert(orderEntity.Details.ToList());
                Remove(carts);
                Save("عملیات با موفقیت صورت گرفت");

                serviceResult.Data = orderEntity.ToDto();
            }

            else serviceResult.AddError(saveResult.Errors.FirstOrDefault());
            return serviceResult;
        }
        public async Task<ServiceResult> SuccessPayment(int orderId)
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

                if (serviceResult.IsSuccess)
                {
                    var user = GetUser(order.UserId);

                    await _smsService.SendSmsForConsomerOrder(new ConsumerOrderModel
                    {
                        toNum = user.PhoneNumber,
                        inputData = new List<InputDataForConsumerOrder>()
                        {
                          new InputDataForConsumerOrder
                          {
                            code=order.Id,
                            name=user.FullName
                         }
                     }
                    });

                    var adminUsers = GetAdminUsers();

                    foreach (var adminUser in adminUsers)
                    {
                        await _smsService.SendSmsForAdminOrder(new AdminOrderModel()
                        {
                            toNum = adminUser.PhoneNumber,
                            inputData = new List<InputDataForAdminOrder>
                          {
                           new InputDataForAdminOrder{  code=order.Id , name=adminUser.FullName}
                          }
                        });
                    }
                }
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
        public async Task<ServiceResult> ChangePassword(ChangePasswordDto dto)
        {
            var serviceResult = dto.IsValid();
            if (serviceResult.IsSuccess)
            {
                var errorKeys = new Dictionary<string, string>();
                errorKeys.Add("PasswordMismatch", "رمز عبور قبلی صحیح نمی باشد");

                var user = GetUser(dto.UserId);
                if (user != null)
                {
                    var identityResult = await _userManager.ChangePasswordAsync(user, dto.OldPassword, dto.NewPassword);
                    if (!identityResult.Succeeded)
                    {
                        var error = errorKeys.Where(c => c.Key == identityResult.Errors.Select(i => i.Code).FirstOrDefault()).FirstOrDefault();
                        serviceResult.AddError(error.Value);
                    }
                }
                else serviceResult.AddError("کاربری یافت نشد");
            }
            return serviceResult;
        }

        public ServiceResult EditProfile(EditProfileDto dto)
        {
            var serviceResult = dto.IsValid();
            if (serviceResult.IsSuccess)
            {
                var user = GetUser(dto.UserId);
                if (user == null)
                    serviceResult.AddError("کاربری یافت نشد");
                else
                {
                    user.FullName = dto.FullName;
                    user.Address = dto.Address;
                    Update(user);
                    serviceResult = Save("عملیات با موفقیت صورت گرفت");
                }
            }
            return serviceResult;
        }
        public async Task<ServiceResult> ForgotPassword(string phoneNumber)
        {
            var serviceResult = new ServiceResult(true);
            var user = GetUserByPhoneNumber(phoneNumber);
            if (user == null)
                serviceResult.AddError("کاربری یافت نشد");
            else
            {
                var newPassword = "";
                for (int i = 0; i < 6; i++)
                {
                    newPassword += new Random().Next(1, 9);
                }

                var hashedNewPassword = _userManager.PasswordHasher.HashPassword(user, newPassword);
                UserStore<User> store = new UserStore<User>(_dbContext);
                await store.SetPasswordHashAsync(user, hashedNewPassword);

                await _smsService.SendSmsForForgotPassword(new ForgotPasswordModel
                {
                    toNum = user.PhoneNumber,
                    inputData = new List<InputDataForForgotPassword>
                  {
                   new InputDataForForgotPassword{   name=user.FullName, password=newPassword}
                  }
                });

            }

            return serviceResult;
        }

        private void GetProduct(List<Domain.Entities.Category> categories)
        {
            if (categories.Count > 0)
            {
                foreach (var cat in categories)
                {
                    _product.AddRange(cat.Products.ToList());
                    List<Domain.Entities.Category> subCats = cat.Children.ToList();
                    GetProduct(subCats);
                }
            }
        }

        public List<Domain.Dto.Category.CategoryDto> GetAllCategories()
        {
            var data = _dbContext
                .Categories
                .Include(c => c.Children)
                .AsEnumerable()
                .Where(c => !c.ParentId.HasValue)
                .ToList();

            return data.ToDto();
        }
        public ServiceResult<CategoryProductsDto> GetProducts(ProductUserSearchDto dto)
        {
            var serviceResult = new ServiceResult<CategoryProductsDto>(true);

            var category = _dbContext
                .Categories
                .AsEnumerable()
                .Where(c => c.Id == dto.CategoryId)
                .ToList()
                .FirstOrDefault();

            if (category == null)
                serviceResult.AddError("دسته بندی یافت نشد");

            else
            {
                _catIds.Add(category.Id);
                if (category.Children != null && category.Children.Count > 0)
                    GetCategoryIds_Recursive(category.Children.ToList());

                var query = _dbContext.Products
                    .Where(c => _catIds.Any(i => i == c.CategoryId))
                    .AsQueryable();

                if (!string.IsNullOrEmpty(dto.Title))
                    query = query.Where(c => c.Title.Contains(dto.Title));

                IOrderedQueryable<Product> orderedQery =
                    query.OrderByDescending(c => c.Id);

                serviceResult.Data = new CategoryProductsDto();
                serviceResult.Data.Products = orderedQery.ToPaginated(dto).ToDto();
                serviceResult.Data.CategoryName = category.Title;
            }
            return serviceResult;
        }

        private void GetCategoryIds_Recursive(List<Category> categories)
        {
            if (categories.Count > 0)
            {
                foreach (var cat in categories)
                {
                    if (!_catIds.Any(c => c == cat.Id))
                        _catIds.Add(cat.Id);

                    if (cat.Children != null)
                    {
                        _catIds.AddRange(cat.Children.Select(c => c.Id).ToList());
                        List<Domain.Entities.Category> subCats = cat.Children.ToList();
                        GetCategoryIds_Recursive(subCats);
                    }
                }
            }
        }

        public PaginationDto<Domain.Dto.Product.ProductDto> SearchProducts(ProductUserSearchDto dto)
        {
            var query = _dbContext.Products
                .AsQueryable();

            if (!string.IsNullOrEmpty(dto.Title))
                query = query.Where(c => c.Title.Contains(dto.Title));

            IOrderedQueryable<Product> orderedQery =
                query.OrderByDescending(c => c.Id);

            return orderedQery.ToPaginated(dto).ToDto();
        }
        public ServiceResult UpdateOrder(int orderId, string idPay_id)
        {
            var serviceResult = new ServiceResult(true);
            var order = _dbContext.Orders.FirstOrDefault(c => c.Id == orderId);
            if (order == null)
                serviceResult.AddError("سفارشی یافت نشد");
            else
            {

            }
            return serviceResult;
        }
        public List<RepresentationDto> GetAllRepresentation()
        {
            var data = _dbContext.Representations.ToList();
            return data.ToDto();
        }
    }
}
