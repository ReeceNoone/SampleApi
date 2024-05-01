using System.Net;
using System.Text;
using Locator.Common.Contracts.Exceptions;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace Locator.Common.Extensions;

public static class HttpClientExtensions
{
    private const string JsonContentType = "application/json";

    public static async Task<HttpResponseMessage> PostAsync<TRequest>(this HttpClient httpClient, string route, TRequest requestModel)
        where TRequest : class
    {
        var json = JsonConvert.SerializeObject(requestModel);
        using var content = new StringContent(json, Encoding.UTF8, JsonContentType);

        var httpResponse = await httpClient.PostAsync(route, content);

        if (!httpResponse.IsSuccessStatusCode)
        {
            throw await HandleUnsuccessfulResponseAsync(route, httpResponse);
        }

        return httpResponse;
    }

    public static async Task<TResponse?> PostAsync<TRequest, TResponse>(this HttpClient client, string requestUri, TRequest request)
        where TRequest : class
        where TResponse : class
    {
        var response = await client.PostAsync(requestUri, request);

        if (response.StatusCode == HttpStatusCode.NoContent)
        {
            return default;
        }

        var result = await response.Content.ReadAsStringAsync();

        return JsonConvert.DeserializeObject<TResponse>(result);
    }

    private static async Task<Exception> HandleUnsuccessfulResponseAsync(string requestUri, HttpResponseMessage response)
    {
#pragma warning disable IDE0072
        return response.StatusCode switch
#pragma warning restore IDE0072
        {
            HttpStatusCode.NotFound => new NotFoundException($"Response for requested URI: {requestUri} returned Not Found"),
            HttpStatusCode.Conflict => new ConflictException($"Response for requested URI: {requestUri} returned Conflict"),
            HttpStatusCode.Forbidden => new ForbiddenAccessException($"Response for requested URI: {requestUri} returned Forbidden"),
            HttpStatusCode.BadRequest => await CreateBadRequestExceptionAsync(requestUri, response),
            HttpStatusCode.Unauthorized => new UnauthorizedException($"Response for requested URI: {requestUri} returned Unauthorized"),
            _ => new ApiException(
                $"Response for requested URI: {requestUri} did not return successfully. Status Code: {response.StatusCode}")
        };
    }

    private static async Task<BadRequestException> CreateBadRequestExceptionAsync(string route, HttpResponseMessage httpResponse)
    {
        var errorDetailsText = await httpResponse.Content.ReadAsStringAsync();
        var errorDetails = string.Empty;

        try
        {
            var validationDetails = JsonConvert.DeserializeObject<ValidationProblemDetails>(errorDetailsText);
            if (validationDetails != null)
            {
                errorDetails = $"{validationDetails.Title}: {string.Join(Environment.NewLine, validationDetails.Errors.SelectMany(e => e.Value))}";
            }
        }
        catch (JsonException)
        {
            errorDetails = errorDetailsText;
        }

        var details = $"Route: {route} returned Bad Request. {errorDetails}".Trim();

        return new BadRequestException(details);
    }
}