using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Locator.Common.WebApi.Swagger.Attributes;

public sealed class CreatedAttribute : ProducesResponseTypeAttribute<CreatedResult>
{
    public CreatedAttribute()
        : base(StatusCodes.Status201Created)
    {
    }
}