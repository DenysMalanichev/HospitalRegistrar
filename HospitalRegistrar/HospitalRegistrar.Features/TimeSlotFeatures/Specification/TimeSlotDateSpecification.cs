using System.Linq.Expressions;
using HospitalRegistrar.Domain.Entities;

namespace HospitalRegistrar.Features.TimeSlotFeatures.Specification;

public class TimeSlotDateSpecification : ISpecification<TimeSlot>
{
    private readonly DateTime _dateTime;
    
    public TimeSlotDateSpecification(DateTime dateTime)
    {
        _dateTime = dateTime;
    }
    
    public Expression<Func<TimeSlot, bool>> Criteria =>
        timeSlot => timeSlot.TimeBegin >= _dateTime && 
                    timeSlot.TimeBegin < _dateTime.AddDays(1);
}