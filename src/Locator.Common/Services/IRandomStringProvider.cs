namespace Locator.Common.Services;

public interface IRandomStringProvider
{
    public string GetRandomString(int bytes);

    public string GetRandomString(int minBytes, int maxBytes);
}