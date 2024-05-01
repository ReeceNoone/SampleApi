using System.Net;

namespace Locator.Common.Contracts.Models;

public interface IResult
{
    public object? CustomState { get; set; }

    public HttpStatusCode Status { get; set; }
}