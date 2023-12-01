using HospitalRegistrar.Domain.Entities;
using LinqKit;

namespace HospitalRegistrar.Application.Interfaces.Repositories;

public interface ITimeSlotsRepository : IGenericRepository<TimeSlot>
{
    Task<IEnumerable<TimeSlot>> GetAvailableTimeSlotsByPredicateAsync(ExpressionStarter<TimeSlot> predicateBuilder);
}