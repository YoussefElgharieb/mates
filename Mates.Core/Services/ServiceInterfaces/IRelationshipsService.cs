using Mates.Core.DTO.RelationshipDTOs;


namespace Mates.Core.ServiceContracts
{
    public interface IRelationshipsService
    {
        public Task CreateRelationshipAsync(CreateRelationshipRequest relationshipCreateRequest);
    }
}
