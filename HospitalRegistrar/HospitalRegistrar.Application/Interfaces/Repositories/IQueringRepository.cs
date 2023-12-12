using System.Linq.Expressions;
using LinqKit;

namespace HospitalRegistrar.Application.Interfaces.Repositories;

public interface IQueryingRepository<T>
{
    IQueryable<T> GetItemsByPredicate(ExpressionStarter<T> predicate, Expression<Func<T, object>> sortBy, bool sortDescending);
}
