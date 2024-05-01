using FluentValidation;
using Locator.Contracts.Requests.Users;
using Locator.Contracts.Validators.Users;

namespace Locator.Application.Features.Users;

public class GetUserByIdCommandValidator : AbstractValidator<GetUserByIdCommand>
{
    public GetUserByIdCommandValidator()
    {
        RuleFor(x => x.UserId)
            .NotNull()
            .NotEmpty();
    }
}