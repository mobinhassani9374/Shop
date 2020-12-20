using Shop.Database.Identity.Entities;
using Shop.Domain.Dto.Account;
using Shop.Domain.Dto.Pagination;
using Shop.Domain.Dto.User;
using System;
using System.Collections.Generic;
using System.Text;

namespace Shop.Services.Mapping
{
    public static class UserMapping
    {
        public static PaginationDto<UserDto> ToDto(this PaginationDto<User> source)
        {
            return new PaginationDto<UserDto>
            {
                Count = source.Count,
                Data = source.Data.ToDto(),
                PageCount = source.PageCount,
                PageNumber = source.PageNumber,
                PageSize = source.PageSize
            };
        }

        public static List<UserDto> ToDto(this List<User> sources)
        {
            var result = new List<UserDto>();
            foreach (var source in sources)
                result.Add(source.ToDto());
            return result;
        }

        public static UserDto ToDto(this User source)
        {
            return new UserDto
            {
                FullName = source.FullName,
                Id = source.Id,
                RegisterDate = source.RegisterDate,
                Type = source.Type,
                PhoneNumber = source.PhoneNumber
            };
        }

        public static User ToEntity(this RegisterDto source)
        {
            return new User
            {
                FullName = source.FullName,
                PhoneNumber = source.PhoneNumber,
                UserName = source.PhoneNumber
            };
        }
    }
}
