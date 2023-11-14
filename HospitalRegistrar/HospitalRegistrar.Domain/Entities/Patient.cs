using HospitalRegistrar.Domain.Common;

namespace HospitalRegistrar.Domain.Entities;

public class Patient : BaseEntity
{
    public int Age { get; set; }

    public string Name { get; set; } = default!;

    public virtual List<Record> Records { get; set; } = default!;
}