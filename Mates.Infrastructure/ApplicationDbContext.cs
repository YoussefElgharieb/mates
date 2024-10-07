using Microsoft.EntityFrameworkCore;
using Mates.Core.Domain.Entities;
using Mates.Infrastructure.Configuration;


namespace Mates.Infrastructure
{
    public class ApplicationDbContext : DbContext
    {
        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<Relationship> Relationships { get; set; }

        public ApplicationDbContext(DbContextOptions options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            new RelationshipEntityTypeConfiguration().Configure(modelBuilder.Entity<Relationship>());
        }
    }
}
