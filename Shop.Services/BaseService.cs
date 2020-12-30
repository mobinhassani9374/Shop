using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Shop.Database;
using Shop.Database.Identity.Entities;
using Shop.Domain.Dto.Pagination;
using Shop.Domain.Dto.User;
using Shop.Domain.Dto.UserAccess;
using Shop.Domain.Enumeration;
using Shop.Services.Mapping;
using Shop.Utility;
using Shop.Utility.Extensions;
using System;
using DNTPersianUtils.Core;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Shop.Domain.Dto.Product;
using Newtonsoft.Json;
using Shop.Domain.Dto.SlideShow;
using Shop.Domain.Dto.Order;
using Shop.Domain.Dto.Info;

namespace Shop.Services
{
    public abstract class BaseService
    {
        protected AppDbContext _dbContext;
        private readonly IHostingEnvironment _env;
        public BaseService(AppDbContext dbContext,
            IHostingEnvironment env)
        {
            _dbContext = dbContext;
            _env = env;
        }
        public PaginationDto<UserDto> GetUsers(SearchUserDto dto)
        {
            var query = _dbContext.Users.Where(c => c.Type != Domain.Enumeration.UserType.Programmer);

            if (!string.IsNullOrEmpty(dto.PhoneNumber))
                query = query.Where(c => c.PhoneNumber.Contains(dto.PhoneNumber));

            if (!string.IsNullOrEmpty(dto.FullName))
                query = query.Where(c => c.FullName.Contains(dto.FullName));

            if (dto.Type.HasValue && dto.Type != 0)
                query = query.Where(c => c.Type == dto.Type.Value);

            IOrderedQueryable<User> orderedQery =
               query.OrderByDescending(c => c.RegisterDate);

            return orderedQery.ToPaginated(dto).ToDto();
        }
        public List<User> GetUsers(List<string> ids)
        {
            return _dbContext.Users.Where(c => ids.Any(i => i == c.Id)).ToList();
        }
        protected User GetUser(string userId)
        {
            return _dbContext.Users.FirstOrDefault(c => c.Id == userId);
        }
        protected ServiceResult Save(string succesMessage)
        {
            var result = new ServiceResult(true);
            if (_dbContext.SaveChanges() > 0)
                result.Message = string.IsNullOrEmpty(succesMessage) ? "عملیات با موفقیت صورت گرفت" : succesMessage;
            else result.AddError("در انجام عملیات خطایی رخ داد");
            return result;
        }
        public ServiceResult<ProductDto> GetProductDetail(int id)
        {
            var serviceResult = new ServiceResult<ProductDto>(true);
            var entity = _dbContext.Products.Find(id);
            if (entity == null)
                serviceResult.AddError("محصولی یافت نشد");
            else
            {
                serviceResult.Data = entity.ToDto();
                serviceResult.Data.MoreImages = new List<string>();
                if (!entity.ImagesJson.IsNullOrEmpty())
                    serviceResult.Data.MoreImages = JsonConvert.DeserializeObject<List<string>>(entity.ImagesJson);
            }
            return serviceResult;
        }
        protected T Insert<T>(T entity)
        {
            _dbContext.Entry(entity).State = EntityState.Added;
            return entity;
        }
        protected List<T> Insert<T>(List<T> entities)
        {
            foreach (var entity in entities)
                Insert(entity);

            return entities;
        }
        protected T Update<T>(T entity)
        {
            _dbContext.Update(entity);
            return entity;
        }
        protected T Remove<T>(T entity)
        {
            _dbContext.Remove(entity);
            return entity;
        }
        protected List<T> Remove<T>(List<T> entities)
        {
            foreach (var entity in entities)
                Remove(entity);
            return entities;
        }
        protected ServiceResult<string> Upload(IFormFile imageFile, FileType fileType)
        {
            var extension = System.IO.Path.GetExtension(imageFile.FileName);
            var fileName = $"{Guid.NewGuid()}{extension}";

            var path = System.IO.Path.Combine(_env.WebRootPath, "Files", fileType.GetFolderName(), fileName);

            var fileStream = new System.IO.FileStream(path,
                System.IO.FileMode.Create);

            imageFile.CopyTo(fileStream);

            fileStream.Close();

            return new ServiceResult<string>(true, fileName);
        }
        protected ServiceResult<string> Upload(IFormFile imageFile, FileType fileType, long length)
        {
            var serviceResult = new ServiceResult<string>();
            if (imageFile.Length > length)
                serviceResult.AddError($"حجم {fileType.GetDisplayName()} نمی تواند بیش از {(length / 1024).ToPersianNumbers()} کیلوبایت باشد");
            else serviceResult = Upload(imageFile, fileType);
            return serviceResult;
        }
        protected ServiceResult DeleteFile(string fileName, FileType fileType)
        {
            var servieResult = new ServiceResult(true);
            var path = System.IO.Path.Combine(_env.WebRootPath, "Files", fileType.GetFolderName(), fileName);
            if (System.IO.File.Exists(path))
                System.IO.File.Delete(path);
            else servieResult.AddError("فایلی یافت نشد");
            return servieResult;
        }
        public List<SlideShowDto> GetAllSlideShows()
        {
            var data = _dbContext
                .SlideShows
                .OrderByDescending(c => c.Id)
                .ToList();

            return data.ToDto();
        }
        protected void SetUsers(PaginationDto<OrderDto> sources, List<User> users)
        {
            sources.Data.ForEach(c =>
            {
                c.User = users.FirstOrDefault(i => i.Id == c.UserId)?.ToDto();
            });
        }
        protected void SetUsers(List<ProductVoteDto> sources, List<User> users)
        {
            sources.ForEach(c =>
            {
                c.User = users.FirstOrDefault(i => i.Id == c.UserId)?.ToDto();
            });
        }
        protected void SetUsers(PaginationDto<Domain.Dto.Home.ContactUsDto> sources, List<User> users)
        {
            sources.Data.ForEach(c =>
            {
                c.User = users.FirstOrDefault(i => i.Id == c.UserId)?.ToDto();
            });
        }

        public InfoDto GetLastInfo()
        {
            var info = _dbContext.Infoes.LastOrDefault();
            if (info == null)
                return InfoDto.CreateNewInstance();
            else
                return info.ToDto();
        }
    }
}
