using FluentValidation;
using ReviewService.Shared.ApiModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace ReviewService.Shared.FluentValidators
{
    public class EvaluationPointItemValidator: AbstractValidator<EvaluationPointApiModel>
    {
        public EvaluationPointItemValidator()
        {
            RuleFor(e => e.Name).NotEmpty().WithMessage("Evaluation point name required!");
        }
    }
}
