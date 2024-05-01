using System.Reflection;
using Locator.Common.WebApi.Extensions;

namespace Locator.Common.WebApi.Assemblies;

public static class AssemblyProvider
{
    private static IEnumerable<Assembly>? _cache;

    public static IEnumerable<Assembly>? GetAndCacheReferencedAssemblies(string startsWith)
    {
        if (_cache is not null && _cache.Any())
        {
            return _cache;
        }

        _cache = Assembly.GetEntryAssembly()?
            .GetPeerReferencedDependencies(startsWith)
            .Where(x => !x.FullName!.Contains("Tests", StringComparison.InvariantCulture));

        return _cache;
    }

    public static IEnumerable<Assembly> GetCachedAssemblies()
    {
        return _cache ?? Enumerable.Empty<Assembly>();
    }

    public static IEnumerable<Assembly> GetAndCacheReferencedAssemblies(Assembly assembly, string startsWith)
    {
        if (_cache is not null && _cache.Any())
        {
            return _cache;
        }

        _cache = assembly
            .GetPeerReferencedDependencies(startsWith)
            .Where(IsTestAssembly);

        return _cache;
    }

    private static bool IsTestAssembly(Assembly x)
    {
        return !(x.FullName?.Contains("Tests", StringComparison.InvariantCulture) ?? false);
    }
}