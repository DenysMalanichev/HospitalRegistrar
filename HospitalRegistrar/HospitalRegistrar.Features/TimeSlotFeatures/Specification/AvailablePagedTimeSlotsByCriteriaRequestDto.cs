namespace HospitalRegistrar.Features.TimeSlotFeatures.Specification;

public class AvailablePagedTimeSlotsByCriteriaRequestDto
{
    public string Specialty { get; set; } = null!;
    
    public string DoctorName { get; set; } = null!;
    
    public DateTime? Date { get; set; } = null;

    public int? CurrentPage { get; set; } = null!;
    
    public bool IsDescending { get; set; }
}