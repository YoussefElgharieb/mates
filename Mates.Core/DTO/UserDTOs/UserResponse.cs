namespace Mates.Core.DTO.UserDTOs
{
    public class UserResponse
    {
        public Guid Id { get; set; }
        public required string Email { get; set; }
        public required string Name { get; set; }
    }
}
