using Locator.Common.Contracts.Models;
using Locator.Contracts.Requests.Locations;
using Locator.Contracts.Responses;
using MediatR;

namespace Locator.Application.Features.Locations;

public class UpdateLocationCommand : IRequest<Result<UserLocationDto>>
{
    public required UpdateUserLocationRequest Request { get; init; }

    public required Guid UserId { get; init; }
}