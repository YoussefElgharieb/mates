using Mates.Core.Domain.Entities;
using Mates.Core.DTO.RelationshipDTOs;


namespace Mates.Core.ServiceContracts
{
    public interface IRelationshipsService
    {
        public Task<RelationshipResponse> CreateRelationshipAsync(RelationshipCreateRequest relationshipCreateRequest);
    }
}
