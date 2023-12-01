namespace HospitalRegistrar.Features.TimeSlotFeatures;

public record CreateTimeSlotDto
{
    public bool IsVacant { get; set; }
    
    public DateTime TimeBegin { get; set; }
    
    public DateTime TimeEnd { get; set; }
}