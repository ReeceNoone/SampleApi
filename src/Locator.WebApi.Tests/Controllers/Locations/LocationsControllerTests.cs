using Locator.Contracts.Responses;
using Locator.Persistence.Entities;
using Locator.WebApi.Tests.Framework;

namespace Locator.WebApi.Tests.Controllers.Locations;

public partial class LocationsControllerTests : ControllerTestBase
{
    public async Task<UserDto> CreateUser(string? firstName = null, string? lastName = null, string? email = null)
    {
        firstName ??= "John";
        lastName ??= "Doe";
        email ??= "john.doe@gmail.com";
        var id = Guid.NewGuid();

        var result = await UserRepository.AddAsync(new UserEntity { Id = id, Email = email, FirstName = firstName, LastName = lastName });

        return new UserDto { Id = result.Id, Email = result.Email, FirstName = result.FirstName, LastName = result.LastName };
    }
}