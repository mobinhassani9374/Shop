﻿using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Shop.Database;
using Shop.Database.Identity.Entities;
using Shop.Domain.Dto.Category;
using Shop.Domain.Dto.Pagination;
using Shop.Domain.Dto.Product;
using Shop.Domain.Dto.User;
using Shop.Domain.Dto.UserAccess;
using Shop.Domain.Enumeration;
using Shop.Services.Mapping;
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
            UserManager<User> userManager) : base(dbContext, env)
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
            var serviceResult = new ServiceResult<CategoryDto>(true);
            var entity = dto.ToEntity();
            _dbContext.Categories.Add(entity);
            if (_dbContext.SaveChanges() > 0)
            {
                serviceResult.Data = entity.ToDto();
            }
            else
            {
                serviceResult.AddError("در انجام عملیات خطایی رخ داد");
            }

            return serviceResult;
        }
        public List<CategoryDto> GetAllCategories()
        {
            var data = _dbContext.Categories
                .ToList()
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

            _dbContext.Categories.Remove(entity);

            if (_dbContext.SaveChanges() > 0)
            {

            }
            else
            {
                serviceResult.AddError("در انجام عملیات خطایی رخ داد");
            }
            return serviceResult;
        }
        public ServiceResult UpdateCategory(UpdateCategoryDto dto)
        {
            var serviceResult = new ServiceResult(true);

            var entity = _dbContext.Categories.Find(dto.Id);

            if (entity == null)
                serviceResult.AddError("دسته بندی یافت نشد");

            else
            {
                entity.Title = dto.Title;

                if (_dbContext.SaveChanges() > 0)
                {

                }
                else
                {
                    serviceResult.AddError("در انجام عملیات خطایی رخ داد");
                }
            }

            return serviceResult;
        }
        public ServiceResult CreateProduct(CreateProductDto dto)
        {
            var serviceResult = new ServiceResult(true);
            var category = _dbContext.Categories.FirstOrDefault();
            dto.ImageName = Upload(dto.ImageFile, FileType.ProductImage);
            var entity = dto.ToEntity();
            entity.CategoryId = category.Id;
            _dbContext.Products.Add(entity);
            if (_dbContext.SaveChanges() > 0)
            {

            }
            else serviceResult.AddError("در انجام عملیات خطایی رخ داد");
            return serviceResult;
        }
        public PaginationDto<ProductDto> GetProducts(ProductSearchDto dto)
        {
            var query = _dbContext.Products
                .Include(c => c.Category)
                .AsQueryable();

            if (!string.IsNullOrEmpty(dto.Title))
                query = query.Where(c => c.Title.Contains(dto.Title));

            return query.ToPaginated(dto).ToDto();
        }
    }
}
