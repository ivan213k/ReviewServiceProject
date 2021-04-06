using FluentValidation;
using ReviewService.Shared.ApiModels;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;

namespace ReviewService.Shared.FluentValidators
{
    public class ImportanceLevelValidator: AbstractValidator<ImportanceLevelApiModel>
    {
        public ImportanceLevelValidator()
        {
            RuleFor(i => i.Name).NotEmpty().WithMessage("Importance level name required!");
            RuleFor(i => i.Color).NotEmpty().NotEqual("#FFFFFF").WithMessage("Importance level color required!");

        }
    }
}
