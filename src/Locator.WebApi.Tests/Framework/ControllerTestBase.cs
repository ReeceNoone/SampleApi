using Locator.Persistence.Entities;
using Locator.Persistence.Repositories;
using Locator.Persistence.Repositories.Implementations;
using Microsoft.Extensions.DependencyInjection;

namespace Locator.WebApi.Tests.Framework;

public class ControllerTestBase : IAsyncLifetime, IDisposable
{
    private readonly LocatorApiFactory _factory;

    public IGenericInMemoryRepository<UserEntity> UserRepository => _factory.Services.GetRequiredService<IGenericInMemoryRepository<UserEntity>>();

    public IGenericInMemoryRepository<LocationEntity> LocationRepository => _factory.Services.GetRequiredService<IGenericInMemoryRepository<LocationEntity>>();

    protected ControllerTestBase()
    {
        _factory = new LocatorApiFactory();
    }

    ~ControllerTestBase()
    {
        Dispose(false);
    }

    public HttpClient CreateClient()
    {
        return _factory.CreateClient();
    }

    public async Task InitializeAsync()
    {
        await _factory.InitializeAsync();
    }

    public Task DisposeAsync()
    {
        return _factory.DisposeAsync();
    }

    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }

    protected virtual void Dispose(bool disposing)
    {
        if (disposing)
        {
            _factory.Dispose();
        }
    }
}