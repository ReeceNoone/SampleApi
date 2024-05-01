using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Locator.Common.WebApi.Swagger.Attributes;

public sealed class UnauthorizedAttribute : ProducesResponseTypeAttribute<UnauthorizedResult>
{
    public UnauthorizedAttribute()
        : base(StatusCodes.Status401Unauthorized)
    {
    }
}