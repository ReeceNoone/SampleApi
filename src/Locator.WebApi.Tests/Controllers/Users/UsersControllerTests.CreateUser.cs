using System.Net;
using FluentAssertions;
using Locator.Contracts.Requests.Users;

namespace Locator.WebApi.Tests.Controllers.Users;

public partial class UsersControllerTests
{
    [Fact]
    public async Task CreateUser_GivenValidData_ReturnsCreatedResultWithCorrectValue()
    {
        // Arrange
        var user = new CreateUserRequest { FirstName = "John", LastName = "Doe", Email = "john.doe@gmail.com" };

        // Act
        var response = await CreateClient()
            .PostAsJsonAsync("/api/users", user);

        // Assert
        var createdAtResult = response.Headers.GetValues("Location").First();
        response.StatusCode.Should().Be(HttpStatusCode.Created);

        createdAtResult.Should().StartWith("/api/users/");

        var createdId = Guid.Parse(createdAtResult.Replace("/api/users/", string.Empty, StringComparison.Ordinal));
        var createdUser = await UserRepository.GetByIdAsync(createdId);

        createdUser.Should().NotBeNull();
        createdUser!.Id.Should().Be(createdId);
        createdUser.Email.Should().Be(user.Email);
        createdUser.FirstName.Should().Be(user.FirstName);
        createdUser.LastName.Should().Be(user.LastName);
    }
}