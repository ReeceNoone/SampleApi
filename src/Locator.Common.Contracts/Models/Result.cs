using System.Net;
using Microsoft.AspNetCore.Mvc;

namespace Locator.Common.Contracts.Models;

public class Result : IResult
{
    public Result()
    {
    }

    public Result(HttpStatusCode status)
    {
        Status = status;
    }

    public object? CustomState { get; set; }

    public ValidationProblemDetails? ValidationErrors => CustomState as ValidationProblemDetails;

    public HttpStatusCode Status { get; set; }

    public static Result<T> BadRequest<T>(string customState)
    {
        return new Result<T>(HttpStatusCode.BadRequest) { CustomState = new ValidationProblemDetails { Title = customState } };
    }

    public static Result<T> BadRequest<T>(ValidationProblemDetails? customState = null)
    {
        return new Result<T>(HttpStatusCode.BadRequest) { CustomState = customState };
    }

    public static Result<T> Created<T>(T value, Uri? location = null)
    {
        return new Result<T>(HttpStatusCode.Created) { Value = value, CustomState = location };
    }

    public static Result<T> InternalServerError<T>(object? customState = null)
    {
        return new Result<T>(HttpStatusCode.InternalServerError) { CustomState = customState };
    }

    public static Result<T> NoContent<T>()
    {
        return new Result<T>(HttpStatusCode.NoContent);
    }

    public static Result<T> NotFound<T>(object? customState = null)
    {
        return new Result<T>(HttpStatusCode.NotFound) { CustomState = customState };
    }

    public static Result<T> Ok<T>(T value)
    {
        return new Result<T>(HttpStatusCode.OK) { Value = value };
    }

    public static Result<T> StatusCode<T>(HttpStatusCode status, object? customState = null)
    {
        return new Result<T>(status) { CustomState = customState };
    }

    public static Result<T> Unauthorized<T>(object? customState = null)
    {
        return new Result<T>(HttpStatusCode.Unauthorized) { CustomState = customState };
    }

    public static Result<T> Conflict<T>(object? customState)
    {
        return new Result<T>(HttpStatusCode.Conflict) { CustomState = customState };
    }

    public bool IsSuccess()
    {
        return (int)Status is >= 200 and <= 299;
    }
}