using Locator.Common.Contracts.Models;
using Locator.Contracts.Responses;
using MediatR;

namespace Locator.Application.Features.Locations;

public class GetAllUserCurrentLocationCommand : IRequest<Result<Dictionary<Guid, UserLocationDto?>>>
{
}