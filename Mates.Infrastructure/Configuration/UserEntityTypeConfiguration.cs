using Mates.Core.Domain.Entities;
using Mates.Core.Domain.Enums;
using Mates.Core.Services;
using Mates.Core.Services.ServiceInterfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Mates.Infrastructure.Configuration
{
    public class UserEntityTypeConfiguration : IEntityTypeConfiguration<User>
    {
        IPasswordService _passwordService;
        public UserEntityTypeConfiguration(IPasswordService passwordService) 
        {
            _passwordService = passwordService ?? throw new ArgumentException(nameof(passwordService));
        }

        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder
                .Property(u => u.Role)
                .HasDefaultValue(Role.User);

            var hashedPassword = _passwordService.Hash("8&xGnGpCASlo$3q");

            builder
                .HasData(new User()
                {
                    Id = Guid.NewGuid(),
                    Email = "marwan@Luftborn",
                    Password = hashedPassword,
                    Name = "marwan",
                    Role = Role.Admin
                });
        }
    }
}
