using Shop.Domain.Dto;
using Shop.Mvc.Models.UserManagement;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Shop.Mvc.Mapping
{
    public static class UserMapping
    {
        public static CreateUserDto ToDto(this CreateUserViewModel source)
        {
            return new CreateUserDto
            {
                ConfirmPassword = source.ConfirmPassword,
                FullName = source.FullName,
                IsManager = source.IsManager,
                Password = source.Password,
                PhoneNumber = source.PhoneNumber
            };
        }
    }
}
