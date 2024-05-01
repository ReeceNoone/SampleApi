using System.Net;

namespace Locator.Common.Contracts.Models;

public class Result<T> : Result
{
    public T? Value { get; set; }

    public Result()
    {
    }

    public Result(HttpStatusCode status)
        : base(status)
    {
        Status = status;
    }
}