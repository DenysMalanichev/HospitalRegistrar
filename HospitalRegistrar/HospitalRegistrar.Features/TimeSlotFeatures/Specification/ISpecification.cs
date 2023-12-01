using System.Linq.Expressions;

namespace HospitalRegistrar.Features.TimeSlotFeatures.Specification;

public interface ISpecification<T>
{
    Expression<Func<T, bool>> Criteria { get; }
}