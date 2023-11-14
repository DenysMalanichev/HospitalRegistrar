using HospitalRegistrar.Domain.Common;

namespace HospitalRegistrar.Domain.Entities;

public class Doctor : BaseEntity
{
    public string Position { get; set; } = default!;

    public string Name { get; set; } = default!;

    public virtual List<TimeSlot> TimeSlots { get; set; } = default!;
}