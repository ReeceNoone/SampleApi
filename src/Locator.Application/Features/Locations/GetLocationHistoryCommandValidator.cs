using FluentValidation;

namespace Locator.Application.Features.Locations;

public class GetLocationHistoryCommandValidator : AbstractValidator<GetUserLocationHistoryCommand>
{
    public GetLocationHistoryCommandValidator()
    {
        RuleFor(x => x.UserId)
            .NotNull()
            .NotEmpty();
    }
}