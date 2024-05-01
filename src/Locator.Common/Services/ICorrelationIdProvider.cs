namespace Locator.Common.Services;

public interface ICorrelationIdProvider
{
    public string CorrelationId { get; }
}