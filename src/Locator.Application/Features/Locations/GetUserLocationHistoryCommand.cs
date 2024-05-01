using Locator.Common.Contracts.Models;
using Locator.Contracts.Responses;
using MediatR;

namespace Locator.Application.Features.Locations;

public class GetUserLocationHistoryCommand : IRequest<Result<IEnumerable<UserLocationDto>>>
{
    public required Guid UserId { get; init; }
}