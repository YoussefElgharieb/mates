using FluentValidation;

namespace Mates.Core.DTO.RelationshipDTOs
{
    public class CreateRelationshipRequestValidator : AbstractValidator<CreateRelationshipRequest>
    {
        public CreateRelationshipRequestValidator() {
            RuleFor(r => r.OtherUserId)
                .NotEmpty().WithMessage("'OtherUserId' is required");
        }
    }
}
