using AutoMapper;
using Locator.Common.Contracts.Models;
using Locator.Contracts.Responses;
using Locator.Domain.Models;
using Locator.Domain.Services;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Locator.Application.Features.Users;

public class GetUserByIdCommandHandler : IRequestHandler<GetUserByIdCommand, Result<UserDto>>
{
    private readonly IUsersService _usersService;
    private readonly ILogger<GetUserByIdCommandHandler> _logger;
    private readonly IMapper _mapper;

    public GetUserByIdCommandHandler(ILogger<GetUserByIdCommandHandler> logger, IUsersService usersService, IMapper mapper)
    {
        _logger = logger;
        _usersService = usersService;
        _mapper = mapper;
    }

    public async Task<Result<UserDto>> Handle(GetUserByIdCommand request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Started getting user by id ({Id})", request.UserId);

        var user = await _usersService.GetByIdAsync(request.UserId);

        if (user is null)
        {
            _logger.LogInformation("User not found by id ({Id})", request.UserId);
            return Result.NotFound<UserDto>("User not found.");
        }

        _logger.LogInformation("Finished getting user by id ({Id}), {@User}", request.UserId, user);

        return Result.Ok(_mapper.Map<UserDto>(user));
    }
}