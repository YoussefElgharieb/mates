using Mates.Core.DTO.RelationshipDTOs;
using Mates.Core.DTO.UserDTOs;


namespace Mates.Core.ServiceContracts
{
    public interface IRelationshipsService
    {
        public Task CreateRelationshipAsync(CreateRelationshipRequest relationshipCreateRequest);
        public Task<List<UserResponse>> GetFriendsAsync();
    }
}
