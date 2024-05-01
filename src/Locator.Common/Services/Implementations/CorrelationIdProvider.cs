namespace Locator.Common.Services.Implementations;

public class CorrelationIdProvider : ICorrelationIdProvider
{
    public string CorrelationId { get; }

    public CorrelationIdProvider(IGuidProvider guidProvider)
    {
        CorrelationId = guidProvider.NewGuid().ToString();
    }
}