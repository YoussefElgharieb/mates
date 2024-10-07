using System.ComponentModel.DataAnnotations;

namespace Mates.Core.Domain.Entities
{
    public class Relationship
    {
        public Guid UserId { get; set; }
        public Guid OtherUserId { get; set; }
    }
}
