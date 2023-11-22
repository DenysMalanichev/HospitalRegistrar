using HospitalRegistrar.Application.Interfaces.Repositories;
using HospitalRegistrar.Domain.Common;
using HospitalRegistrar.Persistence.Context;
using Microsoft.EntityFrameworkCore;

namespace HospitalRegistrar.Persistence.Repositories;

public class GenericRepository<T> : IGenericRepository<T>, IDisposable
    where T : BaseEntity
{
    private readonly DataContext _context;

    private readonly DbSet<T> _table;

    protected GenericRepository(DataContext context)
    {
        _context = context;
        _table = _context.Set<T>();
    }

    public async Task<T>? GetByIdAsync(int id)
    {
        return await _table.FirstOrDefaultAsync(g => g.Id == id);
    }

    public async Task<IEnumerable<T>> GetAllAsync()
    {
        return await _table.ToListAsync();
    }

    public async Task<T> AddAsync(T entity)
    {
        var added = await _table.AddAsync(entity);

        await _context.SaveChangesAsync();
        
        return added.Entity;
    }

    public async Task<T> UpdateAsync(int id, T entity)
    {
        var dbEntity = await GetByIdAsync(id)!;

        if (dbEntity is not null)
        {
            entity.Id = dbEntity.Id;
            _context.Entry(dbEntity).CurrentValues.SetValues(entity);
        }
        
        await _context.SaveChangesAsync();

        return dbEntity!;
    }

    public async Task<T>? DeleteAsync(int id)
    {
        var entity = await GetByIdAsync(id)!;

        if (entity is null)
        {
            return null!;
        }

        _table.Remove(entity);
        
        await _context.SaveChangesAsync();

        return entity;
    }

    public async void Dispose()
    {
        await _context.DisposeAsync();
        GC.SuppressFinalize(this);
    }
}