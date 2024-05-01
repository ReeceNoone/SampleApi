using FluentAssertions;
using Locator.Contracts.Responses;
using Locator.Persistence.Entities;

namespace Locator.WebApi.Tests.Controllers.Locations;

public partial class LocationsControllerTests
{
    [Fact]
    public async Task GetCurrentLocation_GivenValidData_ReturnsOkResultWithCorrectValue()
    {
        // Arrange
        var user = await CreateUser();

        var userLocation = new LocationEntity
        {
            Id = Guid.NewGuid(),
            UserId = user.Id,
            Latitude = 1.0,
            Longitude = 1.0,
            Address = "Some address",
            CreatedAt = DateTime.Now
        };

        await LocationRepository.AddAsync(userLocation);

        // Act
        var response = await CreateClient()
            .GetFromJsonAsync<UserLocationDto>($"/api/locations/{user.Id}/current");

        // Assert
        response.Should().NotBeNull();
        response!.Longitude.Should().Be(userLocation.Longitude);
        response.Latitude.Should().Be(userLocation.Latitude);
        response.Address.Should().Be(userLocation.Address);
        response.CreatedAt.Should().BeCloseTo(userLocation.CreatedAt, TimeSpan.FromSeconds(1));
        response.LeftAt.Should().BeNull();
    }
}