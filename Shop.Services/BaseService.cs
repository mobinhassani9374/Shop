using Shop.Database;
using Shop.Domain.Dto.Pagination;
using Shop.Domain.Dto.User;
using Shop.Services.Mapping;
using Shop.Utility.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Shop.Services
{
    public abstract class BaseService
    {
        protected AppDbContext _dbContext;
        public BaseService(AppDbContext dbContext)
        {
            _dbContext = dbContext;
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
    }
}
