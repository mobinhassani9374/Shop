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

            return query.ToPaginated(dto).ToDto();
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
        protected T Insert<T>(T entity)
        {
            _dbContext.Entry(entity).State = EntityState.Added;
            return entity;
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
    }
}
