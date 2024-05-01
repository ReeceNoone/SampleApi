using System.Reflection;
using Locator.Common.Services;
using Locator.Common.Services.Implementations;
using Locator.Common.WebApi.Mediation;
using Locator.Common.WebApi.Mediation.Implementations;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.Filters;

namespace Locator.Common.WebApi.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection ConfigureSwagger(
        this IServiceCollection services,
        string apiVersion,
        string swaggerApiName,
        Assembly[] assemblies)
    {
        services.AddSwaggerGen(c =>
        {
            c.SwaggerDoc("v1", new OpenApiInfo { Version = apiVersion, Title = swaggerApiName, });

            c.ExampleFilters();

            c.IgnoreObsoleteActions();
            c.IgnoreObsoleteProperties();

            var xmlFiles = assemblies
                .Where(x => x.Location.EndsWith(".dll", StringComparison.InvariantCulture))
                .Select(x => x.Location.Replace(".dll", ".xml", StringComparison.InvariantCulture))
                .Where(File.Exists);

            foreach (var xmlFile in xmlFiles)
            {
                c.IncludeXmlComments(xmlFile, includeControllerXmlComments: true);
            }
        });

        services.AddSwaggerExamplesFromAssemblies(assemblies);

        return services;
    }

    public static IServiceCollection ConfigureCommandBus(
        this IServiceCollection services,
        Assembly[] assemblies)
    {
        services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblies(assemblies));

        services.AddScoped<ICommandBus, CommandBus>();
        services.AddScoped<IEventBus, EventBus>();
        services.AddScoped<ICorrelationIdProvider, CorrelationIdProvider>();

        return services;
    }

    public static IServiceCollection ConfigureAbstractions(
        this IServiceCollection services)
    {
        services.AddSingleton<IRandomStringProvider, RandomStringProvider>();
        services.AddSingleton<IDateTimeProvider, DateTimeProvider>();
        services.AddSingleton<IGuidProvider, GuidProvider>();

        return services;
    }
}