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
            this._relationshipsRepository = relationshipsRepository;
            this._usersRepository = usersRepository;
        }

        public async Task CreateRelationshipAsync(RelationshipCreateRequest relationshipCreateRequest)
        {
            var UserId = relationshipCreateRequest.UserId;
            var OtherUserId = relationshipCreateRequest.OtherUserId; 
            
            var existingUser = await _usersRepository.GetUserByIdAsync(UserId);
            if(existingUser == null)
            {
                throw new BadHttpRequestException($"{nameof(relationshipCreateRequest.UserId)} is not valid");
            }

            var otherExistingUser = await _usersRepository.GetUserByIdAsync(OtherUserId);
            if(otherExistingUser == null)
            {
                throw new BadHttpRequestException($"{nameof(relationshipCreateRequest.OtherUserId)} is not valid");
            }

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
