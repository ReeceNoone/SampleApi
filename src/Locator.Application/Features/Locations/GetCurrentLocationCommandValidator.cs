using FluentValidation;
using Locator.Contracts.Validators.Locations;

namespace Locator.Application.Features.Locations;

public class GetCurrentLocationCommandValidator : AbstractValidator<GetCurrentLocationCommand>
{
    public GetCurrentLocationCommandValidator()
    {
        RuleFor(x => x.UserId)
            .NotNull()
            .NotEmpty();
    }
}