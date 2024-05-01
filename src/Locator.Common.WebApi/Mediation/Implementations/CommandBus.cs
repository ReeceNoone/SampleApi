using Locator.Common.Contracts.Models;
using Locator.Common.Services;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace Locator.Common.WebApi.Mediation.Implementations;

public class CommandBus : ICommandBus
{
    private readonly IMediator _mediator;
    private readonly ICorrelationIdProvider _correlationIdProvider;

    public CommandBus(IMediator mediator, ICorrelationIdProvider correlationIdProvider)
    {
        _mediator = mediator;
        _correlationIdProvider = correlationIdProvider;
    }

    public async Task<dynamic> SendAsync<TCommand, T>(
        TCommand command,
        CommandMetadata metadata)
        where TCommand : class, IRequest<Result<T>>
    {
        if (metadata.Context is not HttpContext req)
        {
            throw new InvalidOperationException($"{nameof(metadata.Context)} is not of type {nameof(HttpContext)}");
        }

        var mediatorResponse = await _mediator.Send(command);

        return mediatorResponse;
    }

    public async Task<Result<T>> SendAsync<TCommand, T>(HttpContext ctx, TCommand command)
        where TCommand : class, IRequest<Result<T>>
    {
        var commandMetadata = new CommandMetadata(DateTime.UtcNow, _correlationIdProvider.CorrelationId, ctx);
        var result = await SendAsync<TCommand, T>(command, commandMetadata);
        return result;
    }
}