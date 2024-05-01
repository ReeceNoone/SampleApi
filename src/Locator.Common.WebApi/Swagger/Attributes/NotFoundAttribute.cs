using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Locator.Common.WebApi.Swagger.Attributes;

public sealed class NotFoundAttribute : ProducesResponseTypeAttribute<NotFoundAttribute>
{
    public NotFoundAttribute()
        : base(StatusCodes.Status404NotFound)
    {
    }
}