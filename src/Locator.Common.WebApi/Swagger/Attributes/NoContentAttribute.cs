using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Locator.Common.WebApi.Swagger.Attributes;

public sealed class NoContentAttribute : ProducesResponseTypeAttribute<NoContentResult>
{
    public NoContentAttribute()
        : base(StatusCodes.Status204NoContent)
    {
    }
}