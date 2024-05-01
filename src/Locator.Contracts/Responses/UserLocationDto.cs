using Bogus;
using Destructurama.Attributed;
using Swashbuckle.AspNetCore.Filters;

namespace Locator.Contracts.Responses;

public class UserLocationDto : IExamplesProvider<UserLocationDto>
{
    [LogMasked]
    public double Latitude { get; set; }

    [LogMasked]
    public double Longitude { get; set; }

    [LogMasked]
    public string? Address { get; set; }

    [LogMasked]
    public DateTime CreatedAt { get; set; }

    [LogMasked]
    public DateTime? LeftAt { get; set; }

    public UserLocationDto GetExamples()
    {
        return new Faker<UserLocationDto>()
            .RuleFor(x => x.Latitude, f => f.Address.Latitude())
            .RuleFor(x => x.Longitude, f => f.Address.Longitude())
            .RuleFor(x => x.Address, f => f.Address.FullAddress())
            .RuleFor(x => x.CreatedAt, f => f.Date.Past())
            .RuleFor(x => x.LeftAt, f => f.Date.Recent());
    }
}