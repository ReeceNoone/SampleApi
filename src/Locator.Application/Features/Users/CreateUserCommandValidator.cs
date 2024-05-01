using FluentValidation;
using Locator.Contracts.Validators.Users;

namespace Locator.Application.Features.Users;

public class CreateUserCommandValidator : AbstractValidator<CreateUserCommand>
{
    public CreateUserCommandValidator()
    {
        RuleFor(x => x.Request)
            .SetValidator(new CreateUserRequestValidator());
    }
}