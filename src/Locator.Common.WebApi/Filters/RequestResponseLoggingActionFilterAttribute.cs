using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;

namespace Locator.Common.WebApi.Filters;

public sealed class RequestResponseLoggingActionFilterAttribute : ActionFilterAttribute
{
    public override void OnActionExecuting(ActionExecutingContext context)
    {
        var logger = context
                .HttpContext
                .RequestServices
                .GetService(typeof(ILogger<RequestResponseLoggingActionFilterAttribute>))
            as ILogger<RequestResponseLoggingActionFilterAttribute>;

        logger?.LogInformation(
            "Request started: {Method} {RequestPath}, Trace Identifier: {TraceId}",
            context.HttpContext.Request.Method,
            context.HttpContext.Request.Path,
            context.HttpContext.TraceIdentifier);

        base.OnActionExecuting(context);
    }

    public override void OnResultExecuted(ResultExecutedContext context)
    {
        var logger = context
            .HttpContext
            .RequestServices
            .GetService(typeof(ILogger<RequestResponseLoggingActionFilterAttribute>))
            as ILogger<RequestResponseLoggingActionFilterAttribute>;

        logger?.LogInformation(
            "Request finished: {StatusCode} {Method} {RequestPath}, Trace Identifier: {TraceId}",
            context.HttpContext.Response.StatusCode,
            context.HttpContext.Request.Method,
            context.HttpContext.Request.Path,
            context.HttpContext.TraceIdentifier);
    }
}