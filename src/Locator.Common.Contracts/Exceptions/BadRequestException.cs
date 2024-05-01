using System.Net;
using System.Runtime.Serialization;

namespace Locator.Common.Contracts.Exceptions;

[Serializable]
public class BadRequestException : Exception, IResponseStatus
{
    private const string DefaultMessage = "Bad Request";

    public HttpStatusCode StatusCode => HttpStatusCode.BadRequest;

    public BadRequestException()
        : this(DefaultMessage)
    {
    }

    public BadRequestException(string message)
        : base(message)
    {
    }

    public BadRequestException(string message, Exception innerException)
        : base(message, innerException)
    {
    }
}