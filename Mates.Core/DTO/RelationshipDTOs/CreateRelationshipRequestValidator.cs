using FluentValidation;

namespace Mates.Core.DTO.RelationshipDTOs
{
    public class CreateRelationshipRequestValidator : AbstractValidator<CreateRelationshipRequest>
    {
        public CreateRelationshipRequestValidator() {
            RuleFor(r => r.UserId)
                .NotEmpty().WithMessage("'UserId' is required");

            RuleFor(r => r.OtherUserId)
                .NotEmpty().WithMessage("'OtherUserId' is required");

            RuleFor(r => r)
                .Must(r => r.UserId != r.OtherUserId)
                .WithMessage("'UserId' and 'OtherUserId' must not be the same.");
        }
    }
}
