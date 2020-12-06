using Microsoft.AspNetCore.Identity;
using Shop.Database;
using Shop.Database.Identity.Entities;
using Shop.Domain.Dto;
using Shop.Domain.Enumeration;
using Shop.Services.Validations;
using Shop.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
    }
}
