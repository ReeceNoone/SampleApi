using Bogus;
using Swashbuckle.AspNetCore.Filters;

namespace Locator.Contracts.Requests.Users;

public class CreateUserRequest : IExamplesProvider<CreateUserRequest>
{
    public required string Email { get; init; }

    public required string FirstName { get; init; }

    public required string LastName { get; init; }

    public CreateUserRequest GetExamples()
    {
        return new Faker<CreateUserRequest>()
            .RuleFor(x => x.Email, f => f.Person.Email)
            .RuleFor(x => x.FirstName, f => f.Person.FirstName)
            .RuleFor(x => x.LastName, f => f.Person.LastName);
    }
}