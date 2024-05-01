using System.Collections.Concurrent;
using System.Linq.Expressions;
using Locator.Persistence.Entities;

namespace Locator.Persistence.Repositories.Implementations;

public class GenericInMemoryRepository<T> : IGenericInMemoryRepository<T>
    where T : IEntity
{
    private readonly ConcurrentDictionary<Guid, T> _entities;

    public GenericInMemoryRepository()
    {
        _entities = new();
    }

    public Task<T?> GetByIdAsync(Guid id)
    {
        _entities.TryGetValue(id, out var entity);
        return Task.FromResult(entity);
    }

    public Task<IEnumerable<T>> GetAllAsync()
    {
        return Task.FromResult(_entities.Values.AsEnumerable());
    }

    public Task<T> AddAsync(T entity)
    {
        _entities.TryAdd(entity.Id, entity);
        return Task.FromResult(entity);
    }

    public Task<T> UpdateAsync(T entity)
    {
        _entities[entity.Id] = entity;
        return Task.FromResult(entity);
    }

    public Task DeleteAsync(Guid id)
    {
        _entities.TryRemove(id, out _);
        return Task.CompletedTask;
    }

    public Task<bool> ExistsAsync(Guid id)
    {
        return Task.FromResult(_entities.ContainsKey(id));
    }

    public Task<bool> ExistsAsync(Expression<Func<T, bool>> predicate)
    {
        return Task.FromResult(_entities.Values.AsQueryable().Any(predicate));
    }

    public Task<IEnumerable<T>> GetWhereAsync(Func<T, bool> func)
    {
        return Task.FromResult(_entities.Values.Where(func));
    }
}