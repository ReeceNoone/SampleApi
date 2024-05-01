namespace Locator.Common.Contracts.Models;

public class CommandMetadata
{
    public DateTime Timestamp { get; }

    public dynamic? Context { get; }

    public CommandMetadata(
        DateTime timestamp,
        string correlationId,
        dynamic? context = null)
    {
        if (string.IsNullOrWhiteSpace(correlationId))
        {
            throw new ArgumentNullException(nameof(correlationId));
        }

        Timestamp = timestamp;
        Context = context;
    }
}