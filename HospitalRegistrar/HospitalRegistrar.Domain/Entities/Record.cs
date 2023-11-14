using HospitalRegistrar.Domain.Common;

namespace HospitalRegistrar.Domain.Entities;

public class Record : BaseEntity
{
    public string Diagnosis { get; set; } = default!;
    
    public int DoctorId { get; set; }
    
    public Doctor Doctor { get; set; } = default!;
    
    public int PatientId { get; set; }

    public virtual Patient Patient { get; set; } = default!;
    
    public int TimeSlotId { get; set; }
    
    public virtual TimeSlot TimeSlot { get; set; } = default!;
}