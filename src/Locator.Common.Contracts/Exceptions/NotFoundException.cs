using System.Net;
using System.Runtime.Serialization;

namespace Locator.Common.Contracts.Exceptions;

[Serializable]
public class NotFoundException : Exception, IResponseStatus
{
    private const string DefaultMessage = "Not found";

    public HttpStatusCode StatusCode => HttpStatusCode.NotFound;

    public NotFoundException()
        : this(DefaultMessage)
    {
    }

    public NotFoundException(string message)
        : base(message)
    {
    }

    public NotFoundException(string message, Exception innerException)
        : base(message, innerException)
    {
    }
}