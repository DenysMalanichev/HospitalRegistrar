using System.Linq.Expressions;
using HospitalRegistrar.Application.Interfaces.Repositories;
using HospitalRegistrar.Domain.Entities;
using HospitalRegistrar.Persistence.Context;
using LinqKit;
using Microsoft.EntityFrameworkCore;

namespace HospitalRegistrar.Persistence.Repositories;

public class TimeSlotRepository : GenericRepository<TimeSlot>, IQueryingRepository<TimeSlot>, ITimeSlotsRepository
{
    private readonly DataContext _context;
    
    public TimeSlotRepository(DataContext context) : base(context)
    {
        _context = context;
    }

    public async Task<IEnumerable<TimeSlot>> GetAvailableTimeSlotsByPredicateAsync(ExpressionStarter<TimeSlot> predicateBuilder)
    {
        return await _context.TimeSlots.AsQueryable()
            .Where(predicateBuilder)
            .ToListAsync();
    }

    public IQueryable<TimeSlot> GetItemsByPredicate(ExpressionStarter<TimeSlot> predicate, Expression<Func<TimeSlot, object>> sortBy, bool sortDescending)
    {
        var query = _context.TimeSlots
            .Include(g => g.Doctors)
            .Where(predicate);

        if (sortBy is not null)
        {
            query = sortDescending
                ? query.OrderByDescending(sortBy)
                : query.OrderBy(sortBy);
        }

        return query;
    }
}