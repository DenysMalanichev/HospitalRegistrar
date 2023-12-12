using HospitalRegistrar.Features.Common;
using HospitalRegistrar.Features.TimeSlotFeatures.Specification;

namespace HospitalRegistrar.Application.Interfaces.Services;

public interface IDataQueryingService<T, TDto>
{
    Task<GenericPagingDto<TDto>> GetPagedEntityByCriteriaAsync(QueryPaginationCriteria<T> queryPaginationCriteria, params ISpecification<T>[] specifications);
}
