using FluentValidation;
using ReviewService.Shared.AuthorizationDtos;
using System;
using System.Collections.Generic;
using System.Text;

namespace ReviewService.Shared.FluentValidators
{
    public class UserForAuthenticationValidator: AbstractValidator<UserForAuthenticationDto>
    {
        public UserForAuthenticationValidator()
        {
            RuleFor(i => i.Email).NotEmpty().WithMessage("Email required!");
            RuleFor(i => i.Password).NotEmpty().WithMessage("Password required!");
        }
    }
}
