using System.Net;

namespace Locator.Common.Contracts.Exceptions;

public interface IResponseStatus
{
    public HttpStatusCode StatusCode { get; }
}