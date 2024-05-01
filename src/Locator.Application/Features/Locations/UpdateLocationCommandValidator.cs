using FluentValidation;
using Locator.Contracts.Validators.Locations;

namespace Locator.Application.Features.Locations;

public class UpdateLocationCommandValidator : AbstractValidator<UpdateLocationCommand>
{
    public UpdateLocationCommandValidator()
    {
        RuleFor(x => x.Request)
            .SetValidator(new UpdateLocationRequestValidator());
    }
}