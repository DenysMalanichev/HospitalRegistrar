using System.Linq.Expressions;
using HospitalRegistrar.Domain.Entities;

namespace HospitalRegistrar.Features.TimeSlotFeatures.Specification;

public class DoctorNameSpecification : ISpecification<TimeSlot>
{
    private readonly string _name;
    
    public DoctorNameSpecification(string name)
    {
        _name = name;
    }
    
    public Expression<Func<TimeSlot, bool>> Criteria =>
        timeSlot => timeSlot.Doctors.Any(d => d.Name == _name);
}