using FluentValidation;
using ReviewService.Shared.ApiModels;

namespace ReviewService.Shared.FluentValidators
{
    public class ReviewTemplateValidator : AbstractValidator<ReviewTemplateApiModel>
    {
        public ReviewTemplateValidator()
        {
            RuleFor(r => r.Name).NotEmpty().WithMessage("Review template name required");
            RuleFor(r => r.EvaluationPointsTemplateId).NotEmpty().WithMessage("Evaluation points required");
            RuleFor(r => r.MidEvaluationPointId).NotEmpty().WithMessage("Mid evaluation point required");
        }
    }
}
