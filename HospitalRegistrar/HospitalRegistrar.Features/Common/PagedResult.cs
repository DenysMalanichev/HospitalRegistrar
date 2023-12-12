namespace HospitalRegistrar.Features.Common;

public class PagedResult<T>
{
    public IQueryable<T> Items { get; set; }

    public int TotalPages { get; set; }
}