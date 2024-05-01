using AutoMapper;
using Locator.Common.Contracts.Models;
using Locator.Common.WebApi.Mediation;
using Locator.Contracts.Events;
using Locator.Contracts.Responses;
using Locator.Domain.Models;
using Locator.Domain.Services;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Locator.Application.Features.Users;

public class CreateUserCommandHandler : IRequestHandler<CreateUserCommand, Result<UserDto>>
{
    private readonly IEventBus _eventBus;
    private readonly ILogger<CreateUserCommandHandler> _logger;
    private readonly IUsersService _usersService;
    private readonly IMapper _mapper;

    public CreateUserCommandHandler(IEventBus eventBus, ILogger<CreateUserCommandHandler> logger, IUsersService usersService, IMapper mapper)
    {
        _eventBus = eventBus;
        _logger = logger;
        _usersService = usersService;
        _mapper = mapper;
    }

    public async Task<Result<UserDto>> Handle(CreateUserCommand request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Started creating user");

        var user = await _usersService.CreateAsync(request.Request);

        _logger.LogInformation("Finished creating user {@User}", user);

        var userDto = _mapper.Map<UserDto>(user);

        _eventBus.Publish(EventIds.Users.UserCreated, userDto);

        return Result.Created(userDto, new Uri($"/api/users/{userDto.Id}", UriKind.Relative));
    }
}