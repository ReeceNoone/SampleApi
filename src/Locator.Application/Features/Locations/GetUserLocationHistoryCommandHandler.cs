using AutoMapper;
using Locator.Common.Contracts.Models;
using Locator.Contracts.Responses;
using Locator.Domain.Services;
using Locator.Persistence.Repositories;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Locator.Application.Features.Locations;

public class GetUserLocationHistoryCommandHandler : IRequestHandler<GetUserLocationHistoryCommand, Result<IEnumerable<UserLocationDto>>>
{
    private readonly ILocationService _locationService;
    private readonly IUserRepository _userRepository;
    private readonly IMapper _mapper;
    private readonly ILogger<GetUserLocationHistoryCommandHandler> _logger;

    public GetUserLocationHistoryCommandHandler(ILocationService locationService, IMapper mapper, IUserRepository userRepository, ILogger<GetUserLocationHistoryCommandHandler> logger)
    {
        _locationService = locationService;
        _mapper = mapper;
        _userRepository = userRepository;
        _logger = logger;
    }

    public async Task<Result<IEnumerable<UserLocationDto>>> Handle(GetUserLocationHistoryCommand request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Started getting user location history by user id ({Id})", request.UserId);

        var user = await _userRepository.GetByIdAsync(request.UserId);

        if (user is null)
        {
            _logger.LogInformation("User not found by id ({Id})", request.UserId);
            return Result.NotFound<IEnumerable<UserLocationDto>>("User not found.");
        }

        var locations = await _locationService.GetHistoryByUserIdAsync(request.UserId);
        var locationDtos = _mapper.Map<IEnumerable<UserLocationDto>>(locations);

        _logger.LogInformation("Finished getting user location history by user id ({Id})", request.UserId);

        return Result.Ok(locationDtos.OrderByDescending(x => x.CreatedAt).AsEnumerable());
    }
}