using Mates.Core.Domain.Entities;
using Mates.Core.Domain.RepositoryInterfaces;
using Mates.Core.DTO.RelationshipDTOs;
using Mates.Core.DTO.UserDTOs;
using Mates.Core.ServiceContracts;
using Mates.Core.Services.ServiceInterfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.SignalR;

namespace Mates.Core.Services
{
    public class RelationshipsService : IRelationshipsService
    {
        private readonly IRelationshipsRepository _relationshipsRepository;
        private readonly IUsersRepository _usersRepository;
        private readonly IUserProvider _userProvider;

        public RelationshipsService (IRelationshipsRepository relationshipsRepository, IUsersRepository usersRepository, IUserProvider userProvider)
        {
            _relationshipsRepository = relationshipsRepository ?? throw new ArgumentNullException(nameof(relationshipsRepository));
            _usersRepository = usersRepository ?? throw new ArgumentNullException(nameof(usersRepository));
            _userProvider = userProvider ?? throw new ArgumentNullException(nameof(userProvider));
        }

        public async Task CreateRelationshipAsync(CreateRelationshipRequest relationshipCreateRequest)
        {
            Guid userId = _userProvider.GetUserId();

            var OtherUserId = relationshipCreateRequest.OtherUserId; 

            var existingRelationship = await _relationshipsRepository.GetRelationshipAsync(userId, OtherUserId);

            if(existingRelationship != null)
            {
                throw new BadHttpRequestException("relationship already exists");
            }

            var relationship = new Relationship()
            {
                UserId = userId,
                OtherUserId = OtherUserId
            };

            await _relationshipsRepository.CreateRelationshipAsync(relationship);

            return;
        }

        public async Task<List<UserResponse>> GetFriendsAsync()
        {
            var userId = _userProvider.GetUserId();
            var friends = await _relationshipsRepository.GetFriendsAsync(userId);

            var userResponses =  friends.Select(u => new UserResponse()
                {
                    Id = u.Id,
                    Name = u.Name,
                    Email = u.Email
                }).ToList();

            return userResponses;
        }
    }
}
