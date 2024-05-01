using System.Net;
using FluentAssertions;
using Locator.Contracts.Requests.Locations;
using Locator.Contracts.Responses;

namespace Locator.WebApi.Tests.Controllers.Locations;

public partial class LocationsControllerTests
{
    [Fact]
    public async Task PutCurrentLocation_GivenValidData_ReturnsOkResultWithCorrectValue()
    {
        // Arrange
        var user = await CreateUser();
        var location = new UpdateUserLocationRequest { Latitude = 1.0, Longitude = 1.0, Address = "Some address" };

        // Act
        var response = await CreateClient()
            .PutAsJsonAsync($"/api/locations/{user.Id}/current", location);

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.OK);

        var result = await response.Content.ReadFromJsonAsync<UserLocationDto>();
        result.Should().NotBeNull();
        result!.Longitude.Should().Be(location.Longitude);
        result.Latitude.Should().Be(location.Latitude);
        result.Address.Should().Be(location.Address);
        result.CreatedAt.Should().BeCloseTo(DateTime.UtcNow, TimeSpan.FromMinutes(1));

        var userLocations = (await LocationRepository.GetWhereAsync(x => x.UserId == user.Id)).ToList();

        userLocations.Should().NotBeEmpty();
        userLocations.Should().HaveCount(1);

        var userLocation = userLocations[0];
        userLocation.UserId.Should().Be(user.Id);
        userLocation.Latitude.Should().Be(location.Latitude);
        userLocation.Longitude.Should().Be(location.Longitude);
        userLocation.Address.Should().Be(location.Address);
        userLocation.CreatedAt.Should().BeCloseTo(DateTime.UtcNow, TimeSpan.FromMinutes(1));
        userLocation.LeftAt.Should().BeNull();
    }
}