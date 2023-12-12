using Microsoft.EntityFrameworkCore;

namespace HospitalRegistrar.Features.Common;

public static class PagingParamsQueryableExtensions
{
    public static async Task<PagedResult<T>> ApplyPagingAsync<T>(this IQueryable<T> query, IPageable pagingParams)
    {
        var pageCount = await query.CountAsync() / pagingParams.ItemsOnPage;

        var items = query.Skip((pagingParams.CurrentPage - 1) * pagingParams.ItemsOnPage)
            .Take(pagingParams.ItemsOnPage);

        return new PagedResult<T> { Items = items, TotalPages = pageCount };
    }
}