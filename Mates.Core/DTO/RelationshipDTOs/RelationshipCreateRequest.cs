namespace Mates.Core.DTO.RelationshipDTOs
{
    public class RelationshipCreateRequest
    {
        public Guid UserId { get; set; }
        public Guid OtherUserId { get; set; }
    }
}
