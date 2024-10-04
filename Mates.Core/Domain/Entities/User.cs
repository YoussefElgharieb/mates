using System.ComponentModel.DataAnnotations;

namespace Mates.Core.Domain.Entities
{
    public class User
    {
        [Key]
        public Guid Id { get; set; }
        public required string Email { get; set; }
        public required string Password { get; set; }
        public required string Name { get; set; }
    }
}
