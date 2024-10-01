using System.ComponentModel.DataAnnotations;

namespace Mates.Core.Domain.Entities
{
    public class User
    {
        [Key]
        public Guid UserId { get; set; }
        public string? Email { get; set; }
        public string? Password { get; set; }
        public string? Name { get; set; }
    }
}
