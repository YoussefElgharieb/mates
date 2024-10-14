using Mates.Core.Domain.Entities;
using Mates.Core.Domain.RepositoryInterfaces;
using Mates.Core.DTO.RelationshipDTOs;
using Mates.Core.DTO.UserDTOs;
using Mates.Core.ServiceContracts;
using Microsoft.AspNetCore.Http;

namespace Mates.Core.Services
{
    public class RelationshipsService : IRelationshipsService
    {
        private readonly IRelationshipsRepository _relationshipsRepository;
        private readonly IUsersRepository _usersRepository;

        public RelationshipsService (IRelationshipsRepository relationshipsRepository, IUsersRepository usersRepository)
        {
            _relationshipsRepository = relationshipsRepository ?? throw new ArgumentNullException(nameof(relationshipsRepository));
            _usersRepository = usersRepository ?? throw new ArgumentNullException(nameof(usersRepository));
        }

        public async Task CreateRelationshipAsync(Guid userId, CreateRelationshipRequest relationshipCreateRequest)
        {
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

        public async Task<List<UserResponse>> GetFriendsAsync(Guid userId)
        {
            var relationships = await _relationshipsRepository.GetFriendsAsync(userId);

            var friends =  relationships.Select(u => u.UserId == userId ? u.OtherUser : u.User).ToList();

            var userResponses =  friends.Select(u => new UserResponse()
                {
                    Id = u.Id,
                    Name = u.Name,
                    Email = u.Email
                }).ToList();

            return userResponses;
        }

        public async Task<List<UserResponse>> GetNonFriendsAsync(Guid userId)
        {
            var relationships = await _relationshipsRepository.GetFriendsAsync(userId);

            var friends = relationships.Select(u => u.UserId == userId ? u.OtherUserId : u.UserId).ToList();

            var users = await _usersRepository.GetAllUsersAsync();

            var nonFriends = users.Where(u => !friends.Contains(userId) && u.Id != userId).ToList();

            var userResponses = nonFriends.Select(u => new UserResponse()
                {
                    Id = u.Id,
                    Name = u.Name,
                    Email = u.Email
                }).ToList();

            return userResponses;
        }
    }
}
