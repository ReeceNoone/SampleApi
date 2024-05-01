using Locator.Common.Contracts.Models;
using Locator.Common.Services;
using Microsoft.ApplicationInsights;
using Newtonsoft.Json;

namespace Locator.Common.WebApi.Mediation.Implementations;

public class EventBus : IEventBus
{
    private readonly TelemetryClient _telemetryClient;
    private readonly ICorrelationIdProvider _correlationIdProvider;

    public EventBus(TelemetryClient telemetryClient, ICorrelationIdProvider correlationIdProvider)
    {
        _telemetryClient = telemetryClient;
        _correlationIdProvider = correlationIdProvider;
    }

    public void Publish(string category)
    {
        var dict = new Dictionary<string, string>
        {
            { "CorrelationId", _correlationIdProvider.CorrelationId },
        };

        _telemetryClient.TrackEvent(category, dict);
    }

    public void Publish<T>(string category, T payload)
        where T : notnull
    {
        var dict = new Dictionary<string, string>
        {
            { "CorrelationId", _correlationIdProvider.CorrelationId },
            { "Payload", JsonConvert.SerializeObject(payload) },
            { "PayloadType", payload.GetType().FullName ?? "Unknown" },
        };

        _telemetryClient.TrackEvent(category, dict);
    }
}