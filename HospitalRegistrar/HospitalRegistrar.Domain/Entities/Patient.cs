using HospitalRegistrar.Domain.Common;

namespace HospitalRegistrar.Domain.Entities;

public class Patient : BaseEntity
{
    public int Age { get; set; }
    
    public int UserId { get; set; }

    public virtual List<Record> Records { get; set; } = default!;

    public virtual User User { get; set; } = default!;
}