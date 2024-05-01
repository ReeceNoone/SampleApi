using System.Linq.Expressions;
using Locator.Persistence.Entities;

namespace Locator.Persistence.Repositories;

public interface IGenericInMemoryRepository<T>
    where T : IEntity
{
    public Task<T?> GetByIdAsync(Guid id);

    public Task<IEnumerable<T>> GetAllAsync();

    public Task<T> AddAsync(T entity);

    public Task<T> UpdateAsync(T entity);

    public Task DeleteAsync(Guid id);

    public Task<bool> ExistsAsync(Guid id);

    public Task<bool> ExistsAsync(Expression<Func<T, bool>> predicate);

    public Task<IEnumerable<T>> GetWhereAsync(Func<T, bool> func);
}