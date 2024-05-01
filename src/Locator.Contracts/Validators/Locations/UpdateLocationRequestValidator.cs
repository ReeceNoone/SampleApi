using FluentValidation;
using Locator.Contracts.Requests.Locations;

namespace Locator.Contracts.Validators.Locations;

public class UpdateLocationRequestValidator : AbstractValidator<UpdateUserLocationRequest>
{
    public UpdateLocationRequestValidator()
    {
        RuleFor(x => x.Latitude)
            .GreaterThanOrEqualTo(-90)
            .LessThanOrEqualTo(90);
        RuleFor(x => x.Longitude)
            .GreaterThanOrEqualTo(-180)
            .LessThanOrEqualTo(180);
    }
}