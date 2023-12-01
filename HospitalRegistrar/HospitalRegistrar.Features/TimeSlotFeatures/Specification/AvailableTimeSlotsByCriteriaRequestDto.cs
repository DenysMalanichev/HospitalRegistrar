namespace HospitalRegistrar.Features.TimeSlotFeatures.Specification;

public class AvailableTimeSlotsByCriteriaRequestDto
{
    public string Specialty { get; set; } = null!;
    public string DoctorName { get; set; } = null!;
    public DateTime? Date { get; set; } = null;
}