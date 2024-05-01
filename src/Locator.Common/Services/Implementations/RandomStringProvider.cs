using System.Security.Cryptography;

namespace Locator.Common.Services.Implementations;

public class RandomStringProvider : IRandomStringProvider
{
    public string GetRandomString(int bytes)
    {
        var data = RandomNumberGenerator.GetBytes(bytes);

        return Base64Utilities.BytesToBase64Url(data);
    }

    public string GetRandomString(int minBytes, int maxBytes)
    {
        var bytes = RandomNumberGenerator.GetInt32(minBytes, maxBytes);
        var data = RandomNumberGenerator.GetBytes(bytes);

        return Base64Utilities.BytesToBase64Url(data);
    }
}