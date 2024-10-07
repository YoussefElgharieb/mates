using FluentValidation;

namespace Mates.Core.DTO.UserDTOs
{
    public class CreateUserRequestValidator : AbstractValidator<CreateUserRequest>
    {
        public CreateUserRequestValidator()
        {
            RuleFor(u => u.Email)
                .NotEmpty().WithMessage("Email is required.")
                .EmailAddress().WithMessage("Email must be in proper format.");
            
            RuleFor(u => u.Password)
                .NotEmpty().WithMessage("Password is required.")
                .MinimumLength(8).WithMessage("Password must be at least 8 characters long.")
                .Matches(@"[A-Z]").WithMessage("Password must contain at least one uppercase letter.")
                .Matches(@"[a-z]").WithMessage("Password must contain at least one lowercase letter.")
                .Matches(@"[0-9]").WithMessage("Password must contain at least one number.")
                .Matches(@"[\W]").WithMessage("Password must contain at least one special character.");
            
            RuleFor(u => u.Name)
                .NotEmpty().WithMessage("Name is required")
                .MinimumLength(1).WithMessage("Name must be at least 8 characters long.")
                .MaximumLength(128).WithMessage("Name cannot be more than 128 character long.");
        }
    }
}
