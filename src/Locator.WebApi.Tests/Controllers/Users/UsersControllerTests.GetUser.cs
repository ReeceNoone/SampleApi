using System.Net;
using FluentAssertions;
using Locator.Contracts.Responses;
using Locator.Persistence.Entities;

namespace Locator.WebApi.Tests.Controllers.Users;

public partial class UsersControllerTests
{
    [Fact]
    public async Task GetUser_GivenValidId_ReturnsOkResultWithCorrectValue()
    {
        // Arrange
        var userId = Guid.NewGuid();
        const string email = "john.doe@gmail.com";
        const string firstName = "John";
        const string lastName = "Doe";

        await UserRepository.AddAsync(new UserEntity { Id = userId, Email = email, FirstName = firstName, LastName = lastName });

        // Act
        var result = await CreateClient()
            .GetAsync($"/api/users/{userId}");

        // Assert
        var user = await result.Content.ReadFromJsonAsync<UserDto>();
        result.StatusCode.Should().Be(HttpStatusCode.OK);

        user.Should().NotBeNull();

        user!.Id.Should().Be(userId);
        user.Email.Should().Be(email);
        user.FirstName.Should().Be(firstName);
        user.LastName.Should().Be(lastName);
    }
}