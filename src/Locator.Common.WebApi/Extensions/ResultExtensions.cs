using System.Net;
using Locator.Common.Contracts.Models;
using Microsoft.AspNetCore.Mvc;

namespace Locator.Common.WebApi.Extensions;

public static class ResultExtensions
{
    public static IActionResult ToActionResult<T>(this Result<T> result)
        where T : class
    {
        return result.Status switch
        {
            HttpStatusCode.OK
                => new OkObjectResult(result.Value),

            HttpStatusCode.Created
                => new CreatedResult(
                    location: result.CustomState == null
                        ? string.Empty
                        : result.CustomState!.ToString() ?? string.Empty,
                    default),

            HttpStatusCode.NoContent
                => new NoContentResult(),

            HttpStatusCode.NotFound
                => result.CustomState == null
                    ? new NotFoundResult()
                    : new NotFoundObjectResult(result.CustomState),

            HttpStatusCode.BadRequest
                => result.CustomState == null
                    ? new BadRequestResult()
                    : new BadRequestObjectResult(result.CustomState),

            HttpStatusCode.InternalServerError
                => new ObjectResult(result.CustomState)
                {
                    StatusCode = (int)HttpStatusCode.InternalServerError,
                },

            HttpStatusCode.Conflict
                => result.CustomState == null
                    ? new ConflictResult()
                    : new ConflictObjectResult(result.CustomState),

            _ => new ObjectResult(result.CustomState)
            {
                StatusCode = (int)result.Status,
            }
        };
    }
}