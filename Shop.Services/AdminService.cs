using Microsoft.AspNetCore.Identity;
using Shop.Database;
using Shop.Database.Identity.Entities;
using Shop.Domain.Dto.User;
using Shop.Domain.Dto.UserAccess;
using Shop.Domain.Enumeration;
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
        public AdminService(AppDbContext dbContext,
            UserManager<User> userManager) : base(dbContext)
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
    }
}
