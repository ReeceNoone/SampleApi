using Locator.Common.Contracts.Models;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace Locator.Common.WebApi.Mediation;

public interface ICommandBus
{
    public Task<dynamic> SendAsync<TCommand, T>(TCommand command, CommandMetadata metadata)
        where TCommand : class, IRequest<Result<T>>;

    public Task<Result<T>> SendAsync<TCommand, T>(
        HttpContext ctx, TCommand command)
        where TCommand : class, IRequest<Result<T>>;
}