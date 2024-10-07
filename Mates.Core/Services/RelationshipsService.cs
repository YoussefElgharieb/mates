using Mates.Core.Domain.Entities;
using Mates.Core.Domain.RepositoryInterfaces;
using Mates.Core.DTO.RelationshipDTOs;
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

        public async Task CreateRelationshipAsync(CreateRelationshipRequest relationshipCreateRequest)
        {
            var UserId = relationshipCreateRequest.UserId;
            var OtherUserId = relationshipCreateRequest.OtherUserId; 

            var existingRelationship = await _relationshipsRepository.GetRelationshipAsync(UserId, OtherUserId);
            if(existingRelationship != null)
            {
                throw new BadHttpRequestException("relationship already exists");
            }

            var relationship = new Relationship()
            {
                UserId = UserId,
                OtherUserId = OtherUserId
            };

            await _relationshipsRepository.CreateRelationshipAsync(relationship);

            return;
        }
    }
}
