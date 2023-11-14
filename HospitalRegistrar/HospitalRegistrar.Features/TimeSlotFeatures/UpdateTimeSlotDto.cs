namespace HospitalRegistrar.Features.TimeSlotFeatures;

public record UpdateTimeSlotDto
{
    public int Id { get; set; }
    
    public DateTime TimeBegin { get; set; }
    
    public DateTime TimeEnd { get; set; }
}