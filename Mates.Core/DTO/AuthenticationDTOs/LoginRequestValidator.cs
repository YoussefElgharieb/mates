using FluentValidation;

namespace Mates.Core.DTO.AuthenticationDTOs
{
    public class LoginRequestValidator : AbstractValidator<LoginRequest>
    {
        public LoginRequestValidator() {
            RuleFor(r => r.Email)
                .NotEmpty()
                .WithMessage("'Email' is required");

            RuleFor(r => r.Password)
                .NotEmpty()
                .WithMessage("'Password' is required");
        }
    }
}
