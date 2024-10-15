using Mates.Core.Domain.Entities;
using Mates.Core.Domain.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Mates.Infrastructure.Configuration
{
    public class UserEntityTypeConfiguration : IEntityTypeConfiguration<User>
    {

        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder
                .Property(u => u.Role)
                .HasDefaultValue(Role.User);

            builder
                .HasData(new User()
                {
                    Id = Guid.NewGuid(),
                    Email = "m.ha@luftborn.com",
                    Password = "$2a$11$PRYh/ESkoB7jNbAsl0z3HefeGscGxvBau7ZL0UBN/FBPuWft7ZC7a",
                    Name = "marwan hamed",
                    Role = Role.Admin
                });

            builder
                .HasMany(u => u.RelationshipsAsUser)
                .WithOne(r => r.User)
                .HasForeignKey(r => r.UserId)
                .OnDelete(DeleteBehavior.NoAction);

            builder
                .HasMany(u => u.RelationshipsAsOtherUser)
                .WithOne(r => r.OtherUser)
                .HasForeignKey(r => r.OtherUserId)
                .OnDelete(DeleteBehavior.NoAction);
        }
    }
}
