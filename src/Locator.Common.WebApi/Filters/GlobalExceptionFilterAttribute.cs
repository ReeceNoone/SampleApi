using FluentValidation.AspNetCore;
using FluentValidation.Results;
using Locator.Common.Contracts.Exceptions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using ValidationException = FluentValidation.ValidationException;

namespace Locator.Common.WebApi.Filters;

public sealed class GlobalExceptionFilterAttribute : ExceptionFilterAttribute
{
    public override void OnException(ExceptionContext context)
    {
        if (context.Exception is ValidationException validationException)
        {
            var validationResult = new ValidationResult(validationException.Errors);
            validationResult.AddToModelState(context.ModelState, null);
            context.Result = new BadRequestObjectResult(context.ModelState);
            context.ExceptionHandled = true;

            return;
        }

        if (context.Exception is not IResponseStatus)
        {
            context.Result = new ObjectResult(new ProblemDetails
            {
                Title = "An error occurred",
                Status = 500,
                Detail = context.HttpContext.TraceIdentifier
            })
            {
                StatusCode = 500
            };
            context.ExceptionHandled = true;
        }
    }

    public override Task OnExceptionAsync(ExceptionContext context)
    {
        OnException(context);
        return Task.CompletedTask;
    }
}