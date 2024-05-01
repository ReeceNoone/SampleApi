using System.ComponentModel.DataAnnotations;
using Bogus;
using Swashbuckle.AspNetCore.Filters;

namespace Locator.Contracts.Requests.Locations;

public class UpdateUserLocationRequest : IExamplesProvider<UpdateUserLocationRequest>
{
    [Required]
    public double Latitude { get; set; }

    [Required]
    public double Longitude { get; set; }

    [Required]
    public string? Address { get; set; }

    public UpdateUserLocationRequest GetExamples()
    {
        return new Faker<UpdateUserLocationRequest>()
            .RuleFor(x => x.Latitude, f => f.Address.Latitude())
            .RuleFor(x => x.Longitude, f => f.Address.Longitude())
            .RuleFor(x => x.Address, f => f.Address.FullAddress());
    }
}