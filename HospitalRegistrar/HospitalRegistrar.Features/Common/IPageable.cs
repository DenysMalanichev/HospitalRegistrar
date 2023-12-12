namespace HospitalRegistrar.Features.Common;

public interface IPageable
{
    public int ItemsOnPage { get; set; }

    public int CurrentPage { get; set; }
}