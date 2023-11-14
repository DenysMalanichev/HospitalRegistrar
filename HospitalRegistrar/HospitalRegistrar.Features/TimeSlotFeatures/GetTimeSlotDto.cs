namespace HospitalRegistrar.Features.TimeSlotFeatures;

public class GetTimeSlotDto
{
    public int Id { get; set; }
    
    public DateTime TimeBegin { get; set; }
    
    public DateTime TimeEnd { get; set; }
    
    public bool IsVacant { get; set; }
}