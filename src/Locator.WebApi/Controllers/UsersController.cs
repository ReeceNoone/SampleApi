using System.Diagnostics.CodeAnalysis;
using Locator.Application.Features.Users;
using Locator.Common.Services;
using Locator.Common.WebApi.Controllers;
using Locator.Common.WebApi.Extensions;
using Locator.Common.WebApi.Mediation;
using Locator.Common.WebApi.Swagger.Attributes;
using Locator.Contracts.Requests.Users;
using Locator.Contracts.Responses;
using Locator.Domain.Models;
using Microsoft.AspNetCore.Mvc;

namespace Locator.WebApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UsersController : BaseApiController
{
    public UsersController(
        ICommandBus commandBus,
        ICorrelationIdProvider correlationIdProvider)
        : base(commandBus, correlationIdProvider)
    {
    }

    [HttpGet("{id:guid}")]
    [Produces<User>]
    public async Task<IActionResult> GetAsync([FromRoute] Guid id)
    {
        var result = await CommandBus.SendAsync<GetUserByIdCommand, UserDto>(HttpContext, new GetUserByIdCommand { UserId = id });

        return result.ToActionResult();
    }

    [HttpPost]
    [Produces<User>]
    public async Task<IActionResult> PostAsync([FromBody] CreateUserRequest request)
    {
        var result = await CommandBus.SendAsync<CreateUserCommand, UserDto>(HttpContext, new CreateUserCommand { Request = request });

        return result.ToActionResult();
    }
}