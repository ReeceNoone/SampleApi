using System.Net;
using System.Runtime.Serialization;

namespace Locator.Common.Contracts.Exceptions;

[Serializable]
public class ApiException : Exception
{
    private const string DefaultMessage = "An error has occurred, your request could not be processed.";

#pragma warning disable CA1822
    public HttpStatusCode StatusCode => HttpStatusCode.InternalServerError;
#pragma warning restore CA1822

    public ApiException()
        : this(DefaultMessage)
    {
    }

    public ApiException(string message)
        : base(message)
    {
    }

    public ApiException(string message, Exception innerException)
        : base(message, innerException)
    {
    }
}