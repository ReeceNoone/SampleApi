using Locator.Domain.Services;
using Locator.Domain.Services.Implementations;
using Microsoft.Extensions.DependencyInjection;

namespace Locator.Domain;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddDomainServices(this IServiceCollection services)
    {
        services.AddScoped<IUsersService, UsersService>();
        services.AddScoped<ILocationService, LocationService>();

        return services;
    }
}