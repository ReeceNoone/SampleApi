using Bogus;
using Destructurama.Attributed;
using Swashbuckle.AspNetCore.Filters;

namespace Locator.Contracts.Responses;

public class UserDto : IExamplesProvider<UserDto>
{
    public Guid Id { get; set; }

    [LogMasked]
    public required string FirstName { get; set; }

    [LogMasked]
    public required string LastName { get; set; }

    [LogMasked]
    public required string Email { get; set; }

    public UserDto GetExamples()
    {
        return new Faker<UserDto>()
            .RuleFor(x => x.Id, f => f.Random.Guid())
            .RuleFor(x => x.Email, f => f.Person.Email)
            .RuleFor(x => x.FirstName, f => f.Person.FirstName)
            .RuleFor(x => x.LastName, f => f.Person.LastName);
    }
}