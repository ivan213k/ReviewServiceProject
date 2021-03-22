using FluentValidation;
using ReviewService.Shared.ApiModels;

namespace ReviewService.Shared.FluentValidators
{
    public class AreaItemValidator : AbstractValidator<AreaItemApiModel>
    {
        public AreaItemValidator()
        {
            RuleFor(a => a.Name).NotEmpty().WithMessage("Area item name required");
        }
    }
}
