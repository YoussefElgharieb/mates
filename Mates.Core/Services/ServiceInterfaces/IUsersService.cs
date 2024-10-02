using Mates.Core.DTO.UserDTOs;

namespace Mates.Core.ServiceContracts
{
    public interface IUsersService
    {
        public Task<UserResponse?> CreateUser(UserCreateRequest userCreateRequest);
    }
}
