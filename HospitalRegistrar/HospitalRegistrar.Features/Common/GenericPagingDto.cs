namespace HospitalRegistrar.Features.Common;

public class GenericPagingDto<T>
{
    public IEnumerable<T> Entities { get; set; } = new List<T>();

    public int TotalPages { get; set; }

    public int CurrentPage { get; set; }
}
