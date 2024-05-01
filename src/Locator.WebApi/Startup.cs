using FluentValidation.AspNetCore;
using Joonasw.AspNetCore.SecurityHeaders;
using Joonasw.AspNetCore.SecurityHeaders.XContentTypeOptions;
using Locator.Common.WebApi.Assemblies;
using Locator.Common.WebApi.Extensions;
using Locator.Common.WebApi.Filters;
using Locator.Domain;
using Locator.Persistence;

namespace Locator.WebApi;

public class Startup
{
    public const string SolutionName = "Locator";
    public const string ApiVersion = "v1";
    public const string V1ApiName = $"{SolutionName} API {ApiVersion}";

    public IConfiguration Configuration { get; }

    public Startup(IConfiguration configuration)
    {
        Configuration = configuration;
    }

    public virtual void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
        if (env.IsDevelopment())
        {
            app
                .UseDeveloperExceptionPage()
                .UseSwagger()
                .UseSwaggerUI(x => x.SwaggerEndpoint($"/swagger/{ApiVersion}/swagger.json", V1ApiName));
        }
        else
        {
            app
                .UseExceptionHandler("/error")
                .UseHsts();
        }

        app
            .UseHttpsRedirection()
            .UseRouting()
            .UseAuthentication()
            .UseAuthorization()
            .UseEndpoints(endpoints => endpoints.MapControllers());

        ApplyContentSecurityPolicies(app);
    }

    public void ConfigureServices(IServiceCollection services)
    {
        var webApiAssembly = typeof(Startup).Assembly;

        services.AddOptions();

        services.ConfigureCommandBus(AssemblyProvider.GetAndCacheReferencedAssemblies(webApiAssembly, SolutionName).ToArray());
        services.ConfigureAbstractions();

        services.AddDomainServices();
        services.AddPersistenceServices();

        services
            .AddControllers()
            .AddApplicationPart(webApiAssembly)
            .AddNewtonsoftJson();

        services
            .AddHttpContextAccessor()
            .AddCsp()
            .AddTransient<GlobalExceptionFilterAttribute>()
            .AddTransient<RequestResponseLoggingActionFilterAttribute>()
            .AddMvc(options =>
            {
                options.Filters.AddService<GlobalExceptionFilterAttribute>();
                options.Filters.AddService<RequestResponseLoggingActionFilterAttribute>();
            })
            .AddControllersAsServices();

        services.ConfigureSwagger(
            V1ApiName,
            ApiVersion,
            AssemblyProvider.GetAndCacheReferencedAssemblies(webApiAssembly, SolutionName).ToArray());

        services.AddAutoMapper(AssemblyProvider.GetAndCacheReferencedAssemblies(webApiAssembly, SolutionName).ToArray());

        services.AddFluentValidationAutoValidation();
        services.AddFluentValidationClientsideAdapters();
        services.AddApplicationInsightsTelemetry();
    }

    private static void ApplyContentSecurityPolicies(IApplicationBuilder builder)
    {
        builder
            .UseMiddleware<XContentTypeOptionsMiddleware>()
            .UseCsp(csp =>
            {
                csp.AllowScripts.FromSelf();
                csp.AllowStyles.FromSelf();
                csp.AllowFonts.FromSelf();
                csp.AllowImages.FromSelf();
                csp.AllowFrames.FromNowhere();
                csp.AllowPrefetch.FromSelf();
                csp.AllowConnections.OnlyOverHttps();
            });
    }
}