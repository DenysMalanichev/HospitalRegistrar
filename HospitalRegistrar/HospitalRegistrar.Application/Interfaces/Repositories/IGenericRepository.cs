using HospitalRegistrar.Domain.Common;

namespace HospitalRegistrar.Application.Interfaces.Repositories;

public interface IGenericRepository<T>
    where T : BaseEntity
{
    Task<T>? GetByIdAsync(int id);

    Task<IEnumerable<T>> GetAllAsync();

    Task<T> AddAsync(T entity);

    Task<T> UpdateAsync(int id, T entity);

    Task<T>? DeleteAsync(int id);
}