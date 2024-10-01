using System.ComponentModel.DataAnnotations;

namespace Mates.Core.Domain.Entities
{
    public class Admin
    {
        [Key]
        public Guid AdminId { get; set; }
        public string? Email { get; set; }
        public string? Password { get; set; }
    }
}
