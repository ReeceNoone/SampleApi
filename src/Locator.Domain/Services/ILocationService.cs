using Locator.Domain.Models;

namespace Locator.Domain.Services;

public interface ILocationService
{
    public Task<(Location? PreviousLocation, Location NewLocation)> UpdateLocationAsync(Guid userId, Location location);

    public Task<Location?> GetCurrentLocationAsync(Guid requestUserId);

    public Task<IEnumerable<Location>> GetHistoryByUserIdAsync(Guid requestUserId);

    public Task<Dictionary<Guid, Location?>> GetAllUsersCurrentLocationsAsync();
}