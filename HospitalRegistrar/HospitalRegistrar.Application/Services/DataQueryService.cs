using AutoMapper;
using HospitalRegistrar.Application.Interfaces.Repositories;
using HospitalRegistrar.Application.Interfaces.Services;
using HospitalRegistrar.Domain.Common;
using HospitalRegistrar.Features.Common;
using HospitalRegistrar.Features.TimeSlotFeatures.Specification;
using LinqKit;

namespace HospitalRegistrar.Application.Services;

public class DataQueryService<T, TDto>
    : IDataQueryingService<T, TDto>
    where T : BaseEntity
{
    private readonly IQueryingRepository<T> _queryingRepository;
    private readonly IMapper _mapper;

    public DataQueryService(IQueryingRepository<T> queryingRepository, IMapper mapper)
    {
        _queryingRepository = queryingRepository;
        _mapper = mapper;
    }

    public async Task<GenericPagingDto<TDto>> GetPagedEntityByCriteriaAsync(QueryPaginationCriteria<T> queryPaginationCriteria, params ISpecification<T>[] specifications)
    {
        var predicate = PredicateBuilder.New<T>(true);

        predicate = specifications.Aggregate(predicate, (current, specification) => current.And(specification.Criteria));

        var pagedResult = await _queryingRepository.GetItemsByPredicate(predicate, queryPaginationCriteria.SortBy, queryPaginationCriteria.IsDescending)
            .ApplyPagingAsync(queryPaginationCriteria);

        return new GenericPagingDto<TDto>
        {
            CurrentPage = queryPaginationCriteria.CurrentPage,
            Entities = _mapper.Map<List<TDto>>(pagedResult.Items),
            TotalPages = pagedResult.TotalPages,
        };
    }
}
