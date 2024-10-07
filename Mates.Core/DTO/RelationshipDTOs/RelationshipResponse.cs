using Mates.Core.DTO.UserDTOs;

namespace Mates.Core.DTO.RelationshipDTOs
{
    public class RelationshipResponse
    {
        public UserDetails User { get; set; }
        public UserDetails OtherUser { get; set; }

        public struct UserDetails
        {
            public Guid Id { get; set; }
            public required string Name { get; set; }
        }
        public RelationshipResponse(Guid UserId, string UserName, Guid OtherUserId, string OtherUserName)
        {
            User = new UserDetails()
            {
                Id = UserId,
                Name = UserName,
            };
            OtherUser = new UserDetails()
            {
                Id = OtherUserId,
                Name = OtherUserName
            };
        }
    }
}
