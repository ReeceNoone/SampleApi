using System.Net;
using System.Runtime.Serialization;

namespace Locator.Common.Contracts.Exceptions;

[Serializable]
public class UnauthorizedException : Exception, IResponseStatus
{
    private const string DefaultMessage = "Unauthorized";

    public HttpStatusCode StatusCode => HttpStatusCode.Unauthorized;

    public UnauthorizedException()
        : this(DefaultMessage)
    {
    }

    public UnauthorizedException(string message)
        : base(message)
    {
    }

    public UnauthorizedException(string message, Exception innerException)
        : base(message, innerException)
    {
    }
}