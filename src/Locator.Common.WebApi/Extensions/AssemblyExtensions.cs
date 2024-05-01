using System.Reflection;

namespace Locator.Common.WebApi.Extensions;

public static class AssemblyExtensions
{
    public static IEnumerable<Assembly> GetReferencedAssemblies(this Assembly assembly, string startsWith)
    {
        return assembly
            .GetReferencedAssemblies()
            .Where(x => x.Name?.StartsWith(startsWith, StringComparison.CurrentCultureIgnoreCase) ?? false)
            .Select(Assembly.Load);
    }

    public static IEnumerable<Assembly> GetPeerReferencedDependencies(this Assembly assembly, string startsWith)
    {
        var assemblies = assembly.GetReferencedAssemblies(startsWith).ToList();
        var peerAssemblies = new List<Assembly>();
        peerAssemblies.AddRange(assemblies);

        foreach (var asm in assemblies.Where(asm => peerAssemblies.All(x => x.FullName != asm.FullName)))
        {
            peerAssemblies.AddRange(asm.GetReferencedAssemblies(startsWith));
        }

        return peerAssemblies;
    }
}