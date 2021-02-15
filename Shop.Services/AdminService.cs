using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Shop.Database;
using Shop.Database.Identity.Entities;
using Shop.Domain.Dto.Category;
using Shop.Domain.Dto.Info;
using Shop.Domain.Dto.Order;
using Shop.Domain.Dto.Pagination;
using Shop.Domain.Dto.Product;
using Shop.Domain.Dto.Representations;
using Shop.Domain.Dto.SlideShow;
using Shop.Domain.Dto.User;
using Shop.Domain.Dto.UserAccess;
using Shop.Domain.Entities;
using Shop.Domain.Enumeration;
using Shop.Services.Mapping;
using Shop.Services.Messaging.FarazSms;
using Shop.Services.Validations;
using Shop.Utility;
using Shop.Utility.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Shop.Services
{
    public class AdminService : BaseService
    {
        private readonly UserManager<User> _userManager;
        public AdminService(AppDbContext dbContext, IHostingEnvironment env,
            UserManager<User> userManager,
             SmsService smsService) : base(dbContext, env, smsService)
        {
            _userManager = userManager;
        }

        public async Task<ServiceResult> CreateUser(CreateUserDto dto)
        {
            var serviceReslt = dto.IsValid();

            if (serviceReslt.IsSuccess)
            {
                var identityResult = await _userManager.CreateAsync(new User
                {
                    FullName = dto.FullName,
                    Type = dto.IsManager ? UserType.Admin : UserType.Manual,
                    PhoneNumber = dto.PhoneNumber,
                    RegisterDate = DateTime.Now,
                    UserName = dto.PhoneNumber
                }, dto.Password);

                if (!identityResult.Succeeded)
                {
                    serviceReslt.Errors = identityResult.Errors.Select(c => c.Code).ToList();
                    serviceReslt.IsSuccess = false;
                }
            }

            return serviceReslt;
        }

        public async Task<ServiceResult<List<UserAccessGroupingDto>>> GetGroupingAccess(string userId)
        {
            var serviceResult = new ServiceResult<List<UserAccessGroupingDto>>(true);

            var user = GetUser(userId);

            if (user == null)
                serviceResult.AddError("کاربری یافت نشد");

            else
            {
                var roles = await _userManager.GetRolesAsync(user);

                var result = new List<UserAccessGroupingDto>();

                result.Add(new UserAccessGroupingDto
                {
                    Title = "مدیریت کاربران",
                    Items = new List<UserAccessItemDto>
                {
                    new UserAccessItemDto
                    {
                          Code= AccessCode.ViewUser,
                          Title=AccessCode.ViewUser.GetDisplayName(),
                          Checked=roles.Any(i=>i==AccessCode.ViewUser.ToString())
                    }
                }
                });

                serviceResult.Data = result;
            }



            return serviceResult;
        }

        public ServiceResult<CategoryDto> CreateCategory(CreateCategoryDto dto)
        {
            var serviceResult = dto.IsValid().AddData<CategoryDto>(null);

            if (serviceResult.IsSuccess)
            {
                if (dto.ParentId.HasValue)
                {
                    if (_dbContext.Products.Any(c => c.CategoryId == dto.ParentId.Value))
                        serviceResult.AddError("امکان اضافه نمودن زیردسته نیست زیرا گروه دارای محصول می باشد");
                }

                if (serviceResult.IsSuccess)
                {
                    var entity = Insert(dto.ToEntity());
                    serviceResult = Save("عملیات با موفقیت صورت گرفت").AddData<CategoryDto>(new CategoryDto
                    {
                        ParentId = entity.ParentId,
                        Title = entity.Title,
                        Id = entity.Id
                    });
                }

            }

            return serviceResult;
        }
        public List<CategoryDto> GetAllCategories()
        {
            var data = _dbContext.Categories
                .Include(c => c.Children)
                .AsEnumerable()
                .Where(c => !c.ParentId.HasValue)
                .ToList();

            return data.ToDto();
        }
        public ServiceResult DeleteCategory(int id)
        {
            var serviceResult = new ServiceResult(true);

            var entity = _dbContext.Categories.Find(id);

            if (entity == null)
                serviceResult.AddError("دسته بندی یافت نشد");

            else
            {
                // آیا زیرگروه دارد؟
                if (_dbContext.Categories.Any(c => c.ParentId == id))
                    serviceResult.AddError("امکان حذف دسته بندی وجود ندارد زیرا دارای چندین زیردسته می باشد");
                else
                {
                    if (_dbContext.Products.Any(c => c.CategoryId == id))
                        serviceResult.AddError("امکان حذف دسته بندی وجود ندارد زیرا دارای چندین محصول می باشد");
                    else
                    {
                        Remove(entity);
                        serviceResult = Save("دسته بندی با موفقیت حذف شد");
                    }
                }
            }

            return serviceResult;
        }
        public ServiceResult UpdateCategory(UpdateCategoryDto dto)
        {
            var serviceResult = dto.IsValid();

            if (serviceResult.IsSuccess)
            {
                var entity = _dbContext.Categories
                    .AsNoTracking()
                    .FirstOrDefault(c => c.Id == dto.Id);

                if (entity == null)
                    serviceResult.AddError("دسته بندی یافت نشد");
                else
                {
                    Update(dto.ToEntity(entity.ParentId));
                    serviceResult = Save("عملیات با موفقیت صورت گرفت");
                }
            }
            return serviceResult;
        }
        public ServiceResult CreateProduct(CreateProductDto dto)
        {
            var serviceResult = dto.IsValid();
            if (serviceResult.IsSuccess)
            {
                var uploadService = Upload(dto.ImageFile, FileType.ProductImage, 500 * 1024);
                if (uploadService.IsSuccess)
                {
                    dto.ImageName = uploadService.Data;
                    Insert(dto.ToEntity());
                    serviceResult = Save("یک محصول با موفقیت ایجاد شد");
                    if (!serviceResult.IsSuccess)
                        DeleteFile(dto.ImageName, FileType.ProductImage);
                }
                else serviceResult.AddError(uploadService.Errors.FirstOrDefault());
            }
            return serviceResult;
        }
        public PaginationDto<ProductDto> GetProducts(ProductSearchDto dto)
        {
            var query = _dbContext.Products
                .Include(c => c.Category)
                .AsQueryable();

            if (!string.IsNullOrEmpty(dto.Title))
                query = query.Where(c => c.Title.Contains(dto.Title));

            IOrderedQueryable<Product> orderedQery =
                query.OrderByDescending(c => c.Id);

            return orderedQery.ToPaginated(dto).ToDto();
        }
        public ProductDto GetProduct(int id)
        {
            var data = _dbContext.Products.Include(c => c.Category).FirstOrDefault(c => c.Id == id);
            return data?.ToDto();
        }
        public ServiceResult UpdateProduct(UpdateProductDto dto)
        {
            var serviceResult = dto.IsValid();

            if (serviceResult.IsSuccess)
            {
                var entity = _dbContext
                    .Products
                    .AsNoTracking()
                    .FirstOrDefault(c => c.Id == dto.Id);

                if (entity == null)
                    serviceResult.AddError("محصولی یافت نشد");
                else
                {
                    if (dto.ImageFile != null)
                    {
                        var uploadServiceResult = Upload(dto.ImageFile, FileType.ProductImage, 500 * 1024);
                        if (uploadServiceResult.IsSuccess)
                        {
                            dto.ImageName = uploadServiceResult.Data;
                            DeleteFile(entity.PrimaryImage, FileType.ProductImage);
                        }
                        else serviceResult.AddError(uploadServiceResult.Errors.FirstOrDefault());
                    }
                    else dto.ImageName = entity.PrimaryImage;

                    Update(dto.ToEntity());
                    serviceResult = Save("محصول با موفقیت ویرایش شد");
                }
            }


            return serviceResult;
        }

        public ServiceResult DeleteProduct(int id)
        {
            var serviceResult = new ServiceResult(true);

            var entity = _dbContext.Products.Find(id);

            if (entity == null)
                serviceResult.AddError("محصولی یافت نشد");

            else
            {
                DeleteFile(entity.PrimaryImage, FileType.ProductImage);

                Remove(entity);

                serviceResult = Save("یک محصول با موفقیت حذف شد");
            }

            return serviceResult;
        }
        public ServiceResult AddNewImage(int productId, IFormFile image)
        {
            var serviceResult = new ServiceResult(true);

            if (image == null)
                serviceResult.AddError("عکس نمی تواند فاقد مقدار باشد");
            else
            {
                var entity = _dbContext
                    .Products
                    .FirstOrDefault(c => c.Id == productId);

                if (entity == null)
                    serviceResult.AddError("محصولی یافت نشد");

                else
                {
                    var uploadService = Upload(image, FileType.ProductImage, 500 * 1024);

                    if (uploadService.IsSuccess)
                    {
                        var imageList = new List<string>();
                        if (!entity.ImagesJson.IsNullOrEmpty())
                            imageList = JsonConvert.DeserializeObject<List<string>>(entity.ImagesJson);
                        imageList.Add(uploadService.Data);
                        entity.ImagesJson = JsonConvert.SerializeObject(imageList);
                        Update(entity);
                        serviceResult = Save("یک عکس با موفقیت اضافه شد");
                        if (!serviceResult.IsSuccess)
                            DeleteFile(entity.ImagesJson, FileType.ProductImage);
                    }
                    else serviceResult.AddError(uploadService.Errors.FirstOrDefault());
                }
            }
            return serviceResult;
        }
        public ServiceResult<List<string>> GetAllImagesForProduct(int id)
        {
            var serviceResult = new ServiceResult<List<string>>(true);
            var entity = _dbContext.Products.Find(id);
            if (entity == null)
                serviceResult.AddError("محصولی یافت نشد");
            else
            {
                if (!entity.ImagesJson.IsNullOrEmpty())
                    serviceResult.Data = JsonConvert.DeserializeObject<List<string>>(entity.ImagesJson);
                else serviceResult.Data = new List<string>();
            }
            return serviceResult;
        }
        public ServiceResult DeleteImageForProduct(int id, string imageGuid)
        {
            var serviceResult = new ServiceResult(true);
            var entity = _dbContext.Products.Find(id);
            if (entity == null)
                serviceResult.AddError("محصولی یافت نشد");
            else
            {
                var imageList = JsonConvert.DeserializeObject<List<string>>(entity.ImagesJson);
                if (imageList.Any(c => c == imageGuid))
                {
                    imageList.Remove(imageGuid);
                    entity.ImagesJson = JsonConvert.SerializeObject(imageList);
                    Update(entity);
                    serviceResult = Save("یک عکس با موفقیت حذف شد");
                    if (serviceResult.IsSuccess)
                        DeleteFile(imageList.ToString(), FileType.ProductImage);
                }
                else serviceResult.AddError("عکسی یافت نشد");
            }
            return serviceResult;
        }
        public ServiceResult CreateSlideShow(CreateSlideShowDto dto)
        {
            var servieResult = dto.IsValid();
            if (servieResult.IsSuccess)
            {
                var uploadResult = Upload(dto.ImageFile, FileType.SlideShowImage, 500 * 1024);
                if (uploadResult.IsSuccess)
                {
                    Insert(dto.ToEntity(uploadResult.Data));
                    servieResult = Save("اسلایدشو با موفقیت ایجاد شد");
                }
                else servieResult.AddError(uploadResult.Errors.FirstOrDefault());
            }
            return servieResult;
        }
        public ServiceResult DeleteSlideShow(int id)
        {
            var serviceResult = new ServiceResult(true);
            var entity = _dbContext.SlideShows.FirstOrDefault(c => c.Id == id);
            if (entity == null)
                serviceResult.AddError("اسایدشو یافت نشد");
            else
            {
                Remove(entity);
                serviceResult = Save("اسلایدشو با موفقت حذف شد");
            }
            return serviceResult;
        }
        public PaginationDto<OrderDto> GetOrders(OrderSearchDto dto)
        {
            var query = _dbContext.Orders
                  .Include(c => c.Details)
                  .ThenInclude(c => c.Product)
                  .AsQueryable()
                  .Where(c => c.IsPaid);

            IOrderedQueryable<Order> orderedQery =
               query.OrderByDescending(c => c.Id);

            var result = orderedQery.ToPaginated(dto).ToDto();

            var users = GetUsers(result.Data.Select(c => c.UserId).ToList());

            SetUsers(result, users);

            return result;
        }
        public List<ProductVoteDto> GetAllWatingComments()
        {
            var data = _dbContext
                .ProductVote
                .Include(c => c.Product)
                .Where(c => c.Stats == VoteState.Wating)
                .ToList();

            var users = GetUsers(data.Select(c => c.UserId).ToList());
            var result = data.ToDto();
            SetUsers(result, users);

            return result;
        }
        public ServiceResult AcceptCommentForProduct(int id)
        {
            var serviceResult = new ServiceResult(true);
            var vote = _dbContext.ProductVote.Find(id);
            if (vote == null)
                serviceResult.AddError("نظری یافت نشد");
            else
            {
                if (vote.Stats == VoteState.Wating)
                {
                    vote.Stats = VoteState.Accepted;
                    Update(vote);
                    serviceResult = Save("عملیات با موفقیت صورت گرفت");
                }
                else serviceResult.AddError("نظر قبلا بررسی شده است");
            }
            return serviceResult;
        }
        public ServiceResult CancelCommentForProduct(int id)
        {
            var serviceResult = new ServiceResult(true);
            var vote = _dbContext.ProductVote.Find(id);
            if (vote == null)
                serviceResult.AddError("نظری یافت نشد");
            else
            {
                if (vote.Stats == VoteState.Wating)
                {
                    vote.Stats = VoteState.Cancel;
                    Update(vote);
                    serviceResult = Save("عملیات با موفقیت صورت گرفت");
                }
                else serviceResult.AddError("نظر قبلا بررسی شده است");
            }
            return serviceResult;
        }
        public int GetCommentWatingCount()
        {
            return _dbContext.ProductVote.Where(c => c.Stats == VoteState.Wating).Count();
        }
        public ServiceResult SaveInfo(InfoDto dto)
        {
            Insert(dto.ToEntity());
            return Save("عملیات با موفقیت صورت گرفت");
        }

        public PaginationDto<Domain.Dto.Home.ContactUsDto> GetContactUs(Domain.Dto.Home.ContactUsSearchDto dto)
        {
            var query = _dbContext.ContactUs
                .AsQueryable();

            IOrderedQueryable<ContactUs> orderedQery =
                query.OrderByDescending(c => c.Id);

            var data = orderedQery.ToPaginated(dto).ToDto();

            var users = GetUsers(data.Data.Select(c => c.UserId).ToList());

            SetUsers(data, users);

            return data;
        }

        public int CountViewToday()
        {
            var nowDate = DateTime.Now.Date;

            var count = _dbContext.LogServices.Where(c => c.CreateDate.Date == nowDate)
                .GroupBy(c => c.IpAddress)
                .Select(c => c.Key)
                .Count();

            return count;
        }
        public int CountViewLastWeek()
        {
            var maxDate = DateTime.Now.Date;
            var minDate = maxDate.AddDays(-7);

            var count = _dbContext.LogServices.Where(c => c.CreateDate.Date >= minDate && c.CreateDate.Date <= maxDate)
                .GroupBy(c => new { c.IpAddress, c.CreateDate.Date })
                .Select(c => c.Key)
                .Count();

            return count;
        }

        public int CountViewTwoLastWeek()
        {
            var maxDate = DateTime.Now.Date;
            var minDate = maxDate.AddDays(-14);

            var count = _dbContext.LogServices.Where(c => c.CreateDate.Date >= minDate && c.CreateDate.Date <= maxDate)
                .GroupBy(c => new { c.IpAddress, c.CreateDate.Date })
                .Select(c => c.Key)
                .Count();

            return count;
        }
        public int CountViewLastMonth()
        {
            var maxDate = DateTime.Now.Date;
            var minDate = maxDate.AddMonths(-1);

            var count = _dbContext.LogServices.Where(c => c.CreateDate.Date >= minDate && c.CreateDate.Date <= maxDate)
                .GroupBy(c => new { c.IpAddress, c.CreateDate.Date })
                .Select(c => c.Key)
                .Count();

            return count;
        }

        public ServiceResult CreateRepresentation(RepresentationCreateDto dto)
        {
            var serviceReslt = dto.IsValid();

            if (serviceReslt.IsSuccess)
            {
                Insert(dto.ToEntity());
                serviceReslt = Save("عملیات با موفقیت انجام شد");
            }

            return serviceReslt;
        }
    }
}
