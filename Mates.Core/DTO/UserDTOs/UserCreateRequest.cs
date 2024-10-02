using FluentValidation;

namespace Mates.Core.DTO.UserDTOs
{
    public class UserCreateRequest
    {
        public string? Email { get; set; }
        public string? Password { get; set; }
        public string? Name { get; set; }
    }

}
