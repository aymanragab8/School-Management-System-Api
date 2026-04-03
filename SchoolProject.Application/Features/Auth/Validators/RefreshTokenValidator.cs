using FluentValidation;
using SchoolProject.Application.Features.Auth.Commands.Models;

namespace SchoolProject.Application.Features.Auth.Commands.Validators
{
    public class RefreshTokenValidator : AbstractValidator<RefreshTokenCommand>
    {
        public RefreshTokenValidator()
        {
            RuleFor(x => x.AccessToken)
                .NotEmpty().WithMessage("Access token is required");

            RuleFor(x => x.RefreshToken)
                .NotEmpty().WithMessage("Refresh token is required");
        }
    }
}