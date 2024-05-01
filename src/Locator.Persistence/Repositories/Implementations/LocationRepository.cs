using Locator.Persistence.Entities;

namespace Locator.Persistence.Repositories.Implementations;

public class LocationRepository : ILocationRepository
{
    private readonly IGenericInMemoryRepository<LocationEntity> _repository;

    public LocationRepository(IGenericInMemoryRepository<LocationEntity> repository)
    {
        _repository = repository;
    }

    public async Task<LocationEntity> CreateAsync(LocationEntity entity)
    {
        return await _repository.AddAsync(entity);
    }

    public async Task<LocationEntity> UpdateAsync(LocationEntity entity)
    {
        return await _repository.UpdateAsync(entity);
    }

    public async Task<LocationEntity?> GetByIdAsync(Guid id)
    {
        return await _repository.GetByIdAsync(id);
    }

    public async Task<IEnumerable<LocationEntity>> GetAllAsync()
    {
        return await _repository.GetAllAsync();
    }

    public async Task<IEnumerable<LocationEntity>> GetByUserIdAsync(Guid userId)
    {
        return await _repository.GetWhereAsync(x => x.UserId == userId);
    }

    public Task<bool> ExistsAsync(Guid userId)
    {
        return _repository.ExistsAsync(x => x.UserId == userId);
    }

    public Task<LocationEntity?> GetLastLocationAsync(Guid userId)
    {
        return _repository.GetWhereAsync(x => x.UserId == userId).ContinueWith(
            task =>
            {
                var locations = task.Result;
                return locations.MaxBy(x => x.CreatedAt);
            }, TaskScheduler.Current);
    }
}