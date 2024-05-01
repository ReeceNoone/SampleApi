using System.ComponentModel.DataAnnotations;

namespace Locator.Domain.Models;

public class Location
{
    public required Guid UserId { get; set; }

    public required double Latitude { get; set; }

    public required double Longitude { get; set; }

    public required DateTime CreatedAt { get; set; }

    public required DateTime? LeftAt { get; set; }

    public required string? Address { get; set; }
}