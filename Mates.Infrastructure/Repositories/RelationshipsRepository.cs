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
    }
}
