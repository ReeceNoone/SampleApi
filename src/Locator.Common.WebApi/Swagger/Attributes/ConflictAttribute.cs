using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Locator.Common.WebApi.Swagger.Attributes;

public sealed class ConflictAttribute : ProducesResponseTypeAttribute<ConflictResult>
{
    public ConflictAttribute()
        : base(StatusCodes.Status409Conflict)
    {
    }
}