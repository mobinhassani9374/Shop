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
            var query = _dbContext.Users.AsQueryable();

            return query.ToPaginated(dto).ToDto();
        }
    }
}
