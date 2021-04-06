using System;
using System.Collections.Generic;
using System.Text;
using FluentValidation;
using ReviewService.Shared.ApiModels;

namespace ReviewService.Shared.FluentValidators
{
    public class EvaluationPointsTemplateValidator: AbstractValidator<EvaluationPointsTemplateApiModel>
    {
        public EvaluationPointsTemplateValidator()
        {
            RuleFor(e => e.Name).NotEmpty().WithMessage("Evaluation point name required!");
        }
    }
}
