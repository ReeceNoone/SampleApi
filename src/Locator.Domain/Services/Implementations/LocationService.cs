using AutoMapper;
using Locator.Common.Services;
using Locator.Domain.Models;
using Locator.Persistence.Entities;
using Locator.Persistence.Repositories;

namespace Locator.Domain.Services.Implementations;

public class LocationService : ILocationService
{
    private readonly ILocationRepository _locationRepository;
    private readonly IUsersService _usersService;
    private readonly IDateTimeProvider _dateTimeProvider;
    private readonly IGuidProvider _guidProvider;
    private readonly IMapper _mapper;

    public LocationService(ILocationRepository locationRepository, IDateTimeProvider dateTimeProvider, IGuidProvider guidProvider, IMapper mapper, IUsersService usersService)
    {
        _locationRepository = locationRepository;
        _dateTimeProvider = dateTimeProvider;
        _guidProvider = guidProvider;
        _mapper = mapper;
        _usersService = usersService;
    }

    public async Task<(Location? PreviousLocation, Location NewLocation)> UpdateLocationAsync(Guid userId, Location location)
    {
        var lastLocation = await _locationRepository.GetLastLocationAsync(userId);

        if (lastLocation is not null)
        {
            lastLocation.LeftAt = _dateTimeProvider.Now;
            await _locationRepository.UpdateAsync(lastLocation);
        }

        var newLocation = await _locationRepository.CreateAsync(new LocationEntity
        {
            Id = _guidProvider.NewGuid(),
            UserId = userId,
            Longitude = location.Longitude,
            Latitude = location.Latitude,
            CreatedAt = _dateTimeProvider.Now,
            LeftAt = null,
            Address = location.Address
        });

        return (_mapper.Map<Location>(lastLocation), _mapper.Map<Location>(newLocation));
    }

    public async Task<Location?> GetCurrentLocationAsync(Guid requestUserId)
    {
        var location = await _locationRepository.GetLastLocationAsync(requestUserId);

        return location is null ? null : _mapper.Map<Location>(location);
    }

    public async Task<IEnumerable<Location>> GetHistoryByUserIdAsync(Guid requestUserId)
    {
        var locations = await _locationRepository.GetByUserIdAsync(requestUserId);

        return _mapper.Map<IEnumerable<Location>>(locations);
    }

    public async Task<Dictionary<Guid, Location?>> GetAllUsersCurrentLocationsAsync()
    {
        var users = await _usersService.GetAllAsync();
        var usersLocations = new Dictionary<Guid, Location?>();

        foreach (var user in users)
        {
            var location = await GetCurrentLocationAsync(user.Id);

            if (location is not null)
            {
                location = null;
            }

            usersLocations.Add(user.Id, location);
        }

        return usersLocations;
    }
}