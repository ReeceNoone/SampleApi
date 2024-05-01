using AutoMapper;
using Locator.Common.Contracts.Models;
using Locator.Contracts.Responses;
using Locator.Domain.Models;
using Locator.Domain.Services;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Locator.Application.Features.Locations;

public class GetAllUserCurrentLocationCommandHandler : IRequestHandler<GetAllUserCurrentLocationCommand, Result<Dictionary<Guid, UserLocationDto?>>>
{
    private readonly ILocationService _locationService;
    private readonly IMapper _mapper;
    private readonly ILogger<GetAllUserCurrentLocationCommandHandler> _logger;

    public GetAllUserCurrentLocationCommandHandler(ILocationService locationService, IMapper mapper, ILogger<GetAllUserCurrentLocationCommandHandler> logger)
    {
        _locationService = locationService;
        _mapper = mapper;
        _logger = logger;
    }

    public async Task<Result<Dictionary<Guid, UserLocationDto?>>> Handle(GetAllUserCurrentLocationCommand request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Started getting all user current locations");

        var locations = await _locationService.GetAllUsersCurrentLocationsAsync();

        var locationDtos = _mapper.Map<Dictionary<Guid, UserLocationDto?>>(locations);

        _logger.LogInformation("Finished getting all user current locations");

        return Result.Ok(locationDtos);
    }
}