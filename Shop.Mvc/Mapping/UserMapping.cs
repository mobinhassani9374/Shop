using Shop.Domain.Dto;
using Shop.Domain.Dto.Pagination;
using Shop.Domain.Dto.User;
using Shop.Mvc.Models.UserManagement;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Shop.Mvc.Models.Pagination;

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

        public static SearchUserDto ToDto(this SearchUserViewModel source)
        {
            return new SearchUserDto
            {
                FullName = source.FullName,
                PageNumber = source.PageNumber,
                PageSize = source.PageSize
            };
        }

        public static PaginationViewModel<UserViewModel> ToViewModel(this PaginationDto<UserDto> source)
        {
            return new PaginationViewModel<UserViewModel>
            {
                PageSize = source.PageSize,
                Count = source.Count,
                PageCount = source.PageCount,
                PageNumber = source.PageNumber,
                Data = source.Data.ToDto()
            };
        }

        public static List<UserViewModel> ToDto(this List<UserDto> sources)
        {
            var result = new List<UserViewModel>();
            foreach (var source in sources)
                result.Add(source.ToDto());
            return result;
        }

        public static UserViewModel ToDto(this UserDto source)
        {
            return new UserViewModel
            {
                FullName = source.FullName,
                Id = source.Id,
                RegisterDate = source.RegisterDate,
                Type = source.Type,
                PhoneNumber = source.PhoneNumber
            };
        }
    }
}
