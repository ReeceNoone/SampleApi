using AutoMapper;
using Locator.Common.Contracts.Models;
using Locator.Contracts.Responses;
using Locator.Domain.Services;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Locator.Application.Features.Locations;

public class GetCurrentLocationCommandHandler : IRequestHandler<GetCurrentLocationCommand, Result<UserLocationDto>>
{
    private readonly ILogger<GetCurrentLocationCommandHandler> _logger;
    private readonly IUsersService _usersService;
    private readonly ILocationService _locationsService;
    private readonly IMapper _mapper;

    public GetCurrentLocationCommandHandler(ILogger<GetCurrentLocationCommandHandler> logger, IUsersService usersService, ILocationService locationsService, IMapper mapper)
    {
        _logger = logger;
        _usersService = usersService;
        _locationsService = locationsService;
        _mapper = mapper;
    }

    public async Task<Result<UserLocationDto>> Handle(GetCurrentLocationCommand request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Started getting current location for user {UserId}", request.UserId);

        var user = await _usersService.GetByIdAsync(request.UserId);

        if (user is null)
        {
            _logger.LogInformation("User not found by id ({Id})", request.UserId);
            return Result.NotFound<UserLocationDto>("User not found.");
        }

        var location = await _locationsService.GetCurrentLocationAsync(request.UserId);

        if (location?.LeftAt.HasValue != false)
        {
            _logger.LogInformation("Location not found for user {UserId}", request.UserId);
            return Result.NotFound<UserLocationDto>("Location not found.");
        }

        _logger.LogInformation("Finished getting current location for user {UserId}, {@Location}", request.UserId, location);

        var userLocationDto = _mapper.Map<UserLocationDto>(location);

        return Result.Ok(userLocationDto);
    }
}