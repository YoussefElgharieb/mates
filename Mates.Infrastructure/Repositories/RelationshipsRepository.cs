using Mates.Core.Domain.Entities;
using Mates.Core.Domain.RepositoryInterfaces;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography.X509Certificates;

namespace Mates.Infrastructure.Repositories
{
    public class RelationshipsRepository : IRelationshipsRepository
    {
        private readonly ApplicationDbContext _context;

        public RelationshipsRepository(ApplicationDbContext context)
        {
            _context = context ?? throw new ArgumentNullException("'context' cannot be null");
        }
        public async Task<Relationship> CreateRelationshipAsync(Relationship relationship)
        {
            _context.Relationships.Add(relationship);
            await _context.SaveChangesAsync();
            return relationship;
        }

        public async Task<Relationship?> GetRelationshipAsync(Guid UserId, Guid OtherUserId)
        {
            return await _context.Relationships.FirstOrDefaultAsync(r => r.UserId == UserId && r.OtherUserId == OtherUserId || r.UserId == OtherUserId && r.OtherUserId == UserId);
        }
    }
}
