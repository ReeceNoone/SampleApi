using Locator.Common.Contracts.Models;
using Locator.Contracts.Responses;
using MediatR;

namespace Locator.Application.Features.Locations;

public class GetCurrentLocationCommand : IRequest<Result<UserLocationDto>>
{
    public required Guid UserId { get; init; }
}