using System.Linq.Expressions;
using HospitalRegistrar.Domain.Entities;

namespace HospitalRegistrar.Features.TimeSlotFeatures.Specification;

public class DoctorSpecialtySpecification : ISpecification<TimeSlot>
{
    private readonly string _specialty;
    
    public DoctorSpecialtySpecification(string specialty)
    {
        _specialty = specialty;
    }
    
    public Expression<Func<TimeSlot, bool>> Criteria =>
        timeSlot => timeSlot.Doctors.Any(d => d.Position == _specialty);
}