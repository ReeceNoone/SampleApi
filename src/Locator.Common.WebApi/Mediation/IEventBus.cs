namespace Locator.Common.WebApi.Mediation;

public interface IEventBus
{
    public void Publish(string category);

    public void Publish<T>(string category, T payload)
        where T : notnull;
}