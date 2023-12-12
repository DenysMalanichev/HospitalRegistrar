using System.Linq.Expressions;

namespace HospitalRegistrar.Features.Common;

public class QueryPaginationCriteria<T> : IPageable
{
    public Expression<Func<T, object>> SortBy { get; set; }

    public bool IsDescending { get; set; }

    public int ItemsOnPage { get; set; }

    public int CurrentPage { get; set; }
}