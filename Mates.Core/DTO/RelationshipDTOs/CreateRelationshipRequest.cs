namespace Mates.Core.DTO.RelationshipDTOs
{
    public class CreateRelationshipRequest
    {
        public Guid UserId { get; set; }
        public Guid OtherUserId { get; set; }
    }
}
