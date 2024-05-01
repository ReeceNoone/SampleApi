using System.Net;
using System.Runtime.Serialization;

namespace Locator.Common.Contracts.Exceptions;

[Serializable]
public class ConflictException : Exception, IResponseStatus
{
    private const string DefaultMessage = "Conflict";

    public HttpStatusCode StatusCode => HttpStatusCode.Conflict;

    public ConflictException()
        : this(DefaultMessage)
    {
    }

    public ConflictException(string message)
        : base(message)
    {
    }

    public ConflictException(string message, Exception innerException)
        : base(message, innerException)
    {
    }
}