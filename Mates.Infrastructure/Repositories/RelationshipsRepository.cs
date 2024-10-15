using Mates.Core.Domain.Entities;
using Mates.Core.Domain.RepositoryInterfaces;
using Microsoft.EntityFrameworkCore;

namespace Mates.Infrastructure.Repositories
{
    public class RelationshipsRepository : IRelationshipsRepository
    {
        private readonly ApplicationDbContext _context;

        public RelationshipsRepository(ApplicationDbContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }
        public async Task<Relationship> CreateRelationshipAsync(Relationship relationship)
        {
            _context.Relationships.Add(relationship);
            await _context.SaveChangesAsync();
            return relationship;
        }

        public async Task<Relationship?> GetRelationshipAsync(Guid userId, Guid otherUserId)
        {
            return await _context.Relationships.FirstOrDefaultAsync(r => r.UserId == userId && r.OtherUserId == otherUserId || r.UserId == otherUserId && r.OtherUserId == userId);
        }

        public async Task<List<User>> GetFriendsAsync(Guid userId)
        {
            var user = await  _context.Users
                .Include(u => u.RelationshipsAsUser)
                    .ThenInclude(r => r.OtherUser)
                .Include(u => u.RelationshipsAsOtherUser)
                    .ThenInclude(r => r.User)
                .FirstOrDefaultAsync(u => u.Id == userId);
            
            var relationshipsAsUser = user.RelationshipsAsUser;
            var relationshipsAsOtherUser = user.RelationshipsAsOtherUser;


            List<User> otherUsersFromRelationshipsAsUser = null;
            if (relationshipsAsUser != null)
            {
                otherUsersFromRelationshipsAsUser = relationshipsAsOtherUser.Select(r => r.OtherUser).ToList();
            }

            List<User> usersFromRelationshipsAsUser = null;
            if (relationshipsAsOtherUser != null)
            {
                usersFromRelationshipsAsUser = relationshipsAsUser.Select(r => r.User).ToList();
            }


            List<User> friends = new List<User>();
            if (otherUsersFromRelationshipsAsUser != null && usersFromRelationshipsAsUser != null)
            {
                friends = otherUsersFromRelationshipsAsUser.Union(usersFromRelationshipsAsUser).ToList();
            }
            else if (otherUsersFromRelationshipsAsUser != null)
            {
                friends = otherUsersFromRelationshipsAsUser;
            }
            else
            {
                friends = usersFromRelationshipsAsUser;
            }

            return friends;
        }


    }
}
