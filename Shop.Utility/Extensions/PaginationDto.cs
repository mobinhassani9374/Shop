using Shop.Domain.Dto;
using Shop.Domain.Dto.Pagination;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Shop.Utility.Extensions
{
    public static class Extension
    {
        public static PaginationDto<TEntity> ToPaginated<TEntity>(this IQueryable<TEntity> query, PagingDto pagingDto) where TEntity : class
        {
            var result = new PaginationDto<TEntity>();

            result.Count = query.Count();

            result.PageNumber = pagingDto.PageNumber;

            result.PageSize = pagingDto.PageSize;

            result.PageCount = result.Count / result.PageSize;

            if (result.Count % result.PageSize > 0) result.PageCount++;

            result.Data = query
                .Skip((result.PageNumber - 1) * result.PageSize)
                .Take(result.PageSize)
                .ToList();

            return result;
        }
    }
}
