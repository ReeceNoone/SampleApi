using Locator.Common.Contracts.Models;
using Locator.Contracts.Requests.Users;
using Locator.Contracts.Responses;
using Locator.Domain.Models;
using MediatR;

namespace Locator.Application.Features.Users;

public class CreateUserCommand : IRequest<Result<UserDto>>
{
    public required CreateUserRequest Request { get; init; }
}