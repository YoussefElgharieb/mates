using Mates.Core.DTO.RelationshipDTOs;
using Mates.Core.DTO.UserDTOs;


namespace Mates.Core.ServiceContracts
{
    public interface IRelationshipsService
    {
        public Task CreateRelationshipAsync(Guid userId, CreateRelationshipRequest relationshipCreateRequest);
        public Task<List<UserResponse>> GetFriendsAsync(Guid userId);

        //public Task<List<UserResponse>> GetNonFriendsAsync(Guid userId);
    }
}
