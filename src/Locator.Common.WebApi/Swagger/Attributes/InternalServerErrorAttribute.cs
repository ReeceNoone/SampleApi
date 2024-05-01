using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Locator.Common.WebApi.Swagger.Attributes;

public sealed class InternalServerErrorAttribute : ProducesResponseTypeAttribute
{
    public InternalServerErrorAttribute()
        : base(StatusCodes.Status500InternalServerError)
    {
    }
}