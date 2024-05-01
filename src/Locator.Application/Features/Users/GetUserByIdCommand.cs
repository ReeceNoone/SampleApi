using Locator.Common.Contracts.Models;
using Locator.Contracts.Responses;
using MediatR;

namespace Locator.Application.Features.Users;

public class GetUserByIdCommand : IRequest<Result<UserDto>>
{
    public required Guid UserId { get; init; }
}