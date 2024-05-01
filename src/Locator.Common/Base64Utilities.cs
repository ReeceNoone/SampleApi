using System.Text;
using Microsoft.AspNetCore.WebUtilities;

namespace Locator.Common;

public static class Base64Utilities
{
    public static byte[] Base64UrlToBytes(string base64Url)
    {
        return WebEncoders.Base64UrlDecode(base64Url);
    }

    public static string BytesToBase64Url(byte[] bytes)
    {
        return WebEncoders.Base64UrlEncode(bytes);
    }

    public static string Base64UrlToString(string base64Url)
    {
        return Encoding.UTF8.GetString(Base64UrlToBytes(base64Url));
    }

    public static string StringToBase64Url(string str)
    {
        return BytesToBase64Url(Encoding.UTF8.GetBytes(str));
    }
}