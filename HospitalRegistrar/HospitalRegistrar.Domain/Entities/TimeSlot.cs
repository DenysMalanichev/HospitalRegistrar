using HospitalRegistrar.Domain.Common;

namespace HospitalRegistrar.Domain.Entities;

public class TimeSlot : BaseEntity
{
    public DateTime TimeBegin { get; set; }
    
    public DateTime TimeEnd { get; set; }
    
    public bool IsVacant { get; set; }

    public virtual List<Doctor> Doctors { get; set; } = default!;

    public virtual Record Record { get; set; } = default!;
}