namespace Locator.Persistence.Entities;

public class LocationEntity : IEntity
{
    public Guid Id { get; set; }

    public Guid UserId { get; set; }

    public double Latitude { get; set; }

    public double Longitude { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime? LeftAt { get; set; }

    public string? Address { get; set; }
}