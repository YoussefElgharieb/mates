using System.ComponentModel.DataAnnotations;
using Mates.Core.Domain.Enums;

namespace Mates.Core.Domain.Entities
{
    public class User
    {
        [Key]
        public Guid Id { get; set; }
        public required string Email { get; set; }
        public required string Password { get; set; }
        public required string Name { get; set; }
        public required Role Role { get; set; }
    }
}
