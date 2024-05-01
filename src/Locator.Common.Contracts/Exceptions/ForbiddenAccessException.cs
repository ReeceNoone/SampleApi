using System.Net;
using System.Runtime.Serialization;

namespace Locator.Common.Contracts.Exceptions;

[Serializable]
public class ForbiddenAccessException : Exception, IResponseStatus
{
    private const string DefaultMessage = "You are not allowed to access this resource";

    public HttpStatusCode StatusCode => HttpStatusCode.Forbidden;

    public ForbiddenAccessException()
        : this(DefaultMessage)
    {
    }

    public ForbiddenAccessException(string message)
        : base(message)
    {
    }

    public ForbiddenAccessException(string message, Exception innerException)
        : base(message, innerException)
    {
    }
}