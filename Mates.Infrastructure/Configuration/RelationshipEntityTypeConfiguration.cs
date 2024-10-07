using Mates.Core.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace Mates.Infrastructure.Configuration
{
    internal class RelationshipEntityTypeConfiguration : IEntityTypeConfiguration<Relationship>
    {
        public void Configure(EntityTypeBuilder<Relationship> builder)
        {
            builder
                .HasKey(r => new { r.UserId, r.OtherUserId });

            builder
                .HasOne<User>()
                .WithMany()
                .HasForeignKey(r => r.UserId)
                .IsRequired();

            builder
                .HasOne<User>()
                .WithMany()
                .HasForeignKey(r => r.OtherUserId)
                .IsRequired();
        }
    }
}
