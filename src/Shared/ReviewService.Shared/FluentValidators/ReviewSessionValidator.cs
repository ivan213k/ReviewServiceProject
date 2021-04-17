using FluentValidation;
using ReviewService.Shared.ApiModels;

namespace ReviewService.Shared.FluentValidators
{
    public class ReviewSessionValidator : AbstractValidator<ReviewSessionApiModel>
    {
        public ReviewSessionValidator()
        {
            RuleFor(r => r.Name).NotEmpty().WithMessage("Review session name required");
            RuleFor(r => r.DueDate).NotEmpty().WithMessage("Due date required");
            RuleFor(r => r.PersonUnderReview).NotEmpty().WithMessage("Person under review required");
            RuleFor(r => r.ReviewManager).NotEmpty().WithMessage("Review master required");
        }
    }
}
