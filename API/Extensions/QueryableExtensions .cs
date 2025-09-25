using System;
using API.Models.Base;
using Microsoft.EntityFrameworkCore;

namespace API.Extensions;

public static class QueryableExtensions
{

    public static async Task<BaseListResult<T>> ToListWithOrderByAndPagingAsync<T>(this IQueryable<T> query,
        BaseParameters param)
    {
        // Default sorting
        var sortBy = string.IsNullOrWhiteSpace(param.SortBy) ? "Id" : param.SortBy;
        var sortOrder = param.SortOrder?.ToLower() == "desc" ? "desc" : "asc";

        if (sortOrder == "desc")
        {
            query = query.OrderByDescending(x => EF.Property<object>(x!, sortBy));
        }
        else
        {
            query = query.OrderBy(x => EF.Property<object>(x!, sortBy));
        }

        // Paging
        var totalRecords = await query.CountAsync();
        var skip = (param.PageNumber - 1) * param.PageSize;
        var pagedData = await query.Skip(skip).Take(param.PageSize).ToListAsync();

        int totalPages;
        if (param.PageSize == 0)
        {
            totalPages = 1; 
        }
        else
        {
            if (totalRecords == 0)
            {
                totalPages = 1;
            }
            else
            {
                // Logika perhitungan modulus 
                int remain = totalRecords % param.PageSize;
                totalPages = remain == 0
                    ? totalRecords / param.PageSize
                    : (totalRecords / param.PageSize) + 1;
            }
        }

        return new BaseListResult<T>
        {
            Items = pagedData,
            PageNumber = param.PageNumber,
            PageSize = param.PageSize,
            TotalRecords = totalRecords,
            TotalPages = totalPages
        };
    }

}
