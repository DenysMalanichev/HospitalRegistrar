using HospitalRegistrar.Application.Interfaces.Repositories;
using HospitalRegistrar.Domain.Entities;
using HospitalRegistrar.Persistence.Context;

namespace HospitalRegistrar.Persistence.Repositories;

public class TimeSlotRepository : GenericRepository<TimeSlot>, ITimeSlotsRepository
{
    public TimeSlotRepository(DataContext context) : base(context)
    {
    }
}