using Locator.Persistence.Entities;
using Locator.Persistence.Repositories;
using Locator.Persistence.Repositories.Implementations;
using Microsoft.Extensions.DependencyInjection;

namespace Locator.Persistence;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddPersistenceServices(this IServiceCollection services)
    {
        services.AddSingleton<IGenericInMemoryRepository<UserEntity>, GenericInMemoryRepository<UserEntity>>();
        services.AddScoped<IUserRepository, UserRepository>();

        services.AddSingleton<IGenericInMemoryRepository<LocationEntity>, GenericInMemoryRepository<LocationEntity>>();
        services.AddScoped<ILocationRepository, LocationRepository>();

        return services;
    }
}