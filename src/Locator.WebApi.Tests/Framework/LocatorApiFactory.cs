using System.Diagnostics.CodeAnalysis;
using FluentValidation.AspNetCore;
using Joonasw.AspNetCore.SecurityHeaders;
using Locator.Common.WebApi.Assemblies;
using Locator.Common.WebApi.Extensions;
using Locator.Common.WebApi.Filters;
using Locator.Domain;
using Locator.Persistence;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;

namespace Locator.WebApi.Tests.Framework;

[SuppressMessage("Roslynator", "RCS1019:Order modifiers", Justification = "Order is correct.")]
public class LocatorApiFactory : WebApplicationFactory<Program>, IAsyncLifetime
{
    public Task InitializeAsync()
    {
        return Task.CompletedTask;
    }

    public new Task DisposeAsync()
    {
        return Task.CompletedTask;
    }

    protected override IHost CreateHost(IHostBuilder builder)
    {
        var webApiAssembly = typeof(Startup).Assembly;

        var configFile = webApiAssembly.Location.Replace($"{webApiAssembly.GetName().Name!}.dll", "appsettings.Test.json", StringComparison.InvariantCulture);

        builder.ConfigureAppConfiguration(config => config.AddJsonFile(configFile, optional: false));

        // builder.ConfigureServices(services =>
        // {
        //     services.AddOptions();
        //
        //     services.ConfigureCommandBus(AssemblyProvider.GetAndCacheReferencedAssemblies(webApiAssembly, Startup.SolutionName).ToArray());
        //     services.ConfigureAbstractions();
        //
        //     services.AddDomainServices();
        //     services.AddPersistenceServices();
        //
        //     services
        //         .AddControllers()
        //         .AddApplicationPart(webApiAssembly)
        //         .AddNewtonsoftJson();
        //
        //     services
        //         .AddHttpContextAccessor()
        //         .AddCsp()
        //         .AddTransient<GlobalExceptionFilterAttribute>()
        //         .AddTransient<RequestResponseLoggingActionFilterAttribute>()
        //         .AddMvc(options =>
        //         {
        //             options.Filters.AddService<GlobalExceptionFilterAttribute>();
        //             options.Filters.AddService<RequestResponseLoggingActionFilterAttribute>();
        //         })
        //         .AddControllersAsServices();
        //
        //     services.ConfigureSwagger(
        //         Startup.V1ApiName,
        //         Startup.ApiVersion,
        //         AssemblyProvider.GetAndCacheReferencedAssemblies(webApiAssembly, Startup.ApiVersion).ToArray());
        //
        //     services.AddAutoMapper(AssemblyProvider.GetAndCacheReferencedAssemblies(webApiAssembly, Startup.ApiVersion).ToArray());
        //
        //     services.AddFluentValidationAutoValidation();
        //     services.AddFluentValidationClientsideAdapters();
        //     services.AddApplicationInsightsTelemetry();
        // });
        return base.CreateHost(builder);
    }
}