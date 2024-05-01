using Locator.Application.Features.Locations;
using Locator.Common.Services;
using Locator.Common.WebApi.Controllers;
using Locator.Common.WebApi.Extensions;
using Locator.Common.WebApi.Mediation;
using Locator.Contracts.Requests.Locations;
using Locator.Contracts.Responses;
using Microsoft.AspNetCore.Mvc;

namespace Locator.WebApi.Controllers;

[ApiController]
[Route("api/[controller]/")]
public class LocationsController : BaseApiController
{
    public LocationsController(ICommandBus commandBus, ICorrelationIdProvider correlationIdProvider)
        : base(commandBus, correlationIdProvider)
    {
    }

    [HttpGet("{userId:guid}/current")]
    public async Task<IActionResult> GetAsync([FromRoute] Guid userId)
    {
        var result = await CommandBus.SendAsync<GetCurrentLocationCommand, UserLocationDto>(HttpContext, new GetCurrentLocationCommand { UserId = userId });

        return result.ToActionResult();
    }

    [HttpPut("{userId:guid}/current")]
    public async Task<IActionResult> PutAsync([FromRoute] Guid userId, [FromBody] UpdateUserLocationRequest request)
    {
        var result = await CommandBus.SendAsync<UpdateLocationCommand, UserLocationDto>(HttpContext, new UpdateLocationCommand { Request = request, UserId = userId });

        return result.ToActionResult();
    }

    [HttpGet("{userId:guid}/history")]
    public async Task<IActionResult> GetHistoryAsync([FromRoute] Guid userId)
    {
        var result = await CommandBus.SendAsync<GetUserLocationHistoryCommand, IEnumerable<UserLocationDto>>(HttpContext, new GetUserLocationHistoryCommand { UserId = userId });

        return result.ToActionResult();
    }

    [HttpGet]
    public async Task<IActionResult> GetAllCurrentLocationsAsync()
    {
        var result = await CommandBus.SendAsync<GetAllUserCurrentLocationCommand, Dictionary<Guid, UserLocationDto?>>(HttpContext, new GetAllUserCurrentLocationCommand());

        return result.ToActionResult();
    }
}