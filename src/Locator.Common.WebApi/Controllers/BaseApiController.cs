using Locator.Common.Services;
using Locator.Common.WebApi.Filters;
using Locator.Common.WebApi.Mediation;
using Locator.Common.WebApi.Swagger.Attributes;
using Microsoft.AspNetCore.Mvc;

namespace Locator.Common.WebApi.Controllers;

[ApiController]
[InternalServerError]
[BadRequest]
[ServiceFilter(typeof(GlobalExceptionFilterAttribute))]
[ServiceFilter(typeof(RequestResponseLoggingActionFilterAttribute))]
public abstract class BaseApiController : Controller
{
    public ICommandBus CommandBus { get; }

    public ICorrelationIdProvider CorrelationIdProvider { get; }

    protected BaseApiController(ICommandBus commandBus, ICorrelationIdProvider correlationIdProvider)
    {
        CommandBus = commandBus;
        CorrelationIdProvider = correlationIdProvider;
    }
}