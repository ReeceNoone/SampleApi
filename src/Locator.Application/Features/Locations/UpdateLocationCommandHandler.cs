using AutoMapper;
using Locator.Common.Contracts.Models;
using Locator.Common.Services;
using Locator.Common.WebApi.Mediation;
using Locator.Contracts.Events;
using Locator.Contracts.Responses;
using Locator.Domain.Models;
using Locator.Domain.Services;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Locator.Application.Features.Locations;

public class UpdateLocationCommandHandler : IRequestHandler<UpdateLocationCommand, Result<UserLocationDto>>
{
    private readonly IEventBus _eventBus;
    private readonly ILogger<UpdateLocationCommandHandler> _logger;
    private readonly IUsersService _usersService;
    private readonly ILocationService _locationsService;
    private readonly IDateTimeProvider _dateTimeProvider;
    private readonly IMapper _mapper;

    public UpdateLocationCommandHandler(IEventBus eventBus, ILogger<UpdateLocationCommandHandler> logger, IMapper mapper, IUsersService usersService, ILocationService locationsService, IDateTimeProvider dateTimeProvider)
    {
        _eventBus = eventBus;
        _logger = logger;
        _mapper = mapper;
        _usersService = usersService;
        _locationsService = locationsService;
        _dateTimeProvider = dateTimeProvider;
    }

    public async Task<Result<UserLocationDto>> Handle(UpdateLocationCommand request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Started updating location for user {UserId}", request.UserId);

        var user = await _usersService.GetByIdAsync(request.UserId);

        if (user is null)
        {
            _logger.LogInformation("User not found by id ({Id})", request.UserId);
            return Result.NotFound<UserLocationDto>("User not found.");
        }

        var (previousLocation, newLocation) = await _locationsService.UpdateLocationAsync(
            request.UserId,
            new Location
            {
                UserId = request.UserId,
                Longitude = request.Request.Longitude,
                Latitude = request.Request.Latitude,
                Address = request.Request.Address,
                CreatedAt = _dateTimeProvider.Now,
                LeftAt = null
            });

        if (previousLocation is not null)
        {
            _logger.LogInformation("User {UserId} left location {@Location}", request.UserId, previousLocation);
            _eventBus.Publish(EventIds.Locations.LocationLeft, _mapper.Map<UserLocationDto>(previousLocation));
        }

        _logger.LogInformation("Finished updating location for user {UserId}, {@Location}", request.UserId, newLocation);

        var userLocationDto = _mapper.Map<UserLocationDto>(newLocation);
        _eventBus.Publish(EventIds.Locations.LocationUpdated, userLocationDto);

        return Result.Ok(userLocationDto);
    }
}