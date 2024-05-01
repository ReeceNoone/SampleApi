using FluentAssertions;
using Locator.Contracts.Responses;
using Locator.Persistence.Entities;

namespace Locator.WebApi.Tests.Controllers.Locations;

public partial class LocationsControllerTests
{
    [Fact]
    public async Task GetLocationHistory_GivenValidData_ReturnsOkResultWithCorrectValue()
    {
        // Arrange
        var user = await CreateUser();

        var firstLocation = new LocationEntity
        {
            Id = Guid.NewGuid(),
            UserId = user.Id,
            Latitude = 1.0,
            Longitude = 1.0,
            Address = "Some address",
            CreatedAt = DateTime.Now.Subtract(TimeSpan.FromMinutes(5)),
            LeftAt = DateTime.Now.Subtract(TimeSpan.FromMinutes(4))
        };

        var secondLocation = new LocationEntity
        {
            Id = Guid.NewGuid(),
            UserId = user.Id,
            Latitude = 2.0,
            Longitude = 2.0,
            Address = "Some address",
            CreatedAt = DateTime.Now.Subtract(TimeSpan.FromMinutes(1)),
        };

        await LocationRepository.AddAsync(firstLocation);
        await LocationRepository.AddAsync(secondLocation);

        // Act
        var response = (await CreateClient()
            .GetFromJsonAsync<IEnumerable<UserLocationDto>>($"/api/locations/{user.Id}/history") ?? Array.Empty<UserLocationDto>()).ToList();

        // Assert
        response.Should().HaveCount(2);

        // Reverse the order of the locations, because the API returns them in descending order
        var firstLocationDto = response[1];
        var secondLocationDto = response[0];

        firstLocationDto.Latitude.Should().Be(firstLocationDto.Latitude);
        firstLocationDto.Longitude.Should().Be(firstLocationDto.Longitude);
        firstLocationDto.Address.Should().Be(firstLocationDto.Address);
        firstLocationDto.CreatedAt.Should().BeCloseTo(firstLocationDto.CreatedAt, TimeSpan.FromSeconds(1));
        firstLocationDto.LeftAt.Should().BeCloseTo(firstLocationDto.LeftAt!.Value, TimeSpan.FromSeconds(1));

        secondLocationDto.Latitude.Should().Be(secondLocationDto.Latitude);
        secondLocationDto.Longitude.Should().Be(secondLocationDto.Longitude);
        secondLocationDto.Address.Should().Be(secondLocationDto.Address);
        secondLocationDto.CreatedAt.Should().BeCloseTo(secondLocationDto.CreatedAt, TimeSpan.FromSeconds(1));
        secondLocationDto.LeftAt.Should().BeNull();
    }
}