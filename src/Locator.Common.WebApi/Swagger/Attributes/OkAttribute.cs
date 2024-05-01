using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Locator.Common.WebApi.Swagger.Attributes;

public sealed class OkAttribute : ProducesResponseTypeAttribute<OkResult>
{
    public OkAttribute(Type type)
        : base(StatusCodes.Status200OK)
    {
    }
}