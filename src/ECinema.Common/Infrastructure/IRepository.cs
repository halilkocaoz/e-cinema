using System.Linq.Expressions;
using ECinema.Common.Infrastructure.Models;

namespace ECinema.Common.Infrastructure;

public interface IRepository<T>  where T : MongoEntity
{
    IQueryable<T> Get(Expression<Func<T, bool>> predicate = null);
    Task<T> GetAsync(Expression<Func<T, bool>> predicate);
    Task<T> GetByIdAsync(string id);
    Task<T> AddAsync(T entity);
    Task<T> UpdateAsync(T entity);
    Task<T> DeleteAsync(T entity);
}