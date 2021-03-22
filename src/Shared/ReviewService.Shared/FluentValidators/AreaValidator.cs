using FluentValidation;
using ReviewService.Shared.ApiModels;

namespace ReviewService.Shared.FluentValidators
{
    public class AreaValidator : AbstractValidator<AreaApiModel>
    {
        public AreaValidator()
        {
            RuleFor(a => a.Name).NotEmpty().WithMessage("Area name required");
        }
    }
}
