namespace HospitalRegistrar.Features.RecordFeatures;

public record CreateRecordDto
{
    public string Diagnosis { get; set; } = default!;
    
    public int DoctorId { get; set; }
    
    public int PatientId { get; set; }
    
    public int TimeSlotId { get; set; }
}