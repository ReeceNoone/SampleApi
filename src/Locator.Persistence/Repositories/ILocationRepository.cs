using Locator.Persistence.Entities;

namespace Locator.Persistence.Repositories;

public interface ILocationRepository
{
    public Task<LocationEntity> CreateAsync(LocationEntity entity);

    public Task<LocationEntity> UpdateAsync(LocationEntity entity);

    public Task<LocationEntity?> GetByIdAsync(Guid id);

    public Task<IEnumerable<LocationEntity>> GetAllAsync();

    public Task<IEnumerable<LocationEntity>> GetByUserIdAsync(Guid userId);

    public Task<bool> ExistsAsync(Guid userId);

    public Task<LocationEntity?> GetLastLocationAsync(Guid userId);
}