using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Locator.Common.WebApi.Swagger.Attributes;

public sealed class BadRequestAttribute : ProducesResponseTypeAttribute<BadRequestResult>
{
    public BadRequestAttribute()
        : base(StatusCodes.Status400BadRequest)
    {
    }
}