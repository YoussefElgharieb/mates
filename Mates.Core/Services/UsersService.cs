using Mates.Core.Domain.RepositoryContracts;
using Mates.Core.ServiceContracts;
using Mates.Core.Services.ServiceInterfaces;
using Mates.Core.Domain.Entities;
using Mates.Core.DTO.UserDTOs;

namespace Mates.Core.Services
{
    public class UsersService : IUsersService
    {
        private readonly IUsersRepository _userRepository;
        private readonly IPasswordService _passwordService;

        public UsersService(IUsersRepository userRepository, IPasswordService passwordService) 
        { 
            _userRepository = userRepository;
            _passwordService = passwordService;
        }

        public async Task<UserResponse?> CreateUser(UserCreateRequest userCreateRequest)
        {
            //check if a user with the same email already exists
            var userWithSameEmail = await _userRepository.GetUser(userCreateRequest.Email);
            if(userWithSameEmail != null) throw new ArgumentException("A user with the same email already exists");
            
            //hashpassowrd
            var hashedPassword = _passwordService.Hash(userCreateRequest.Password);

            //create user
            var user = new User() 
            { 
                Id = Guid.NewGuid(), 
                Email = userCreateRequest.Email, 
                Password = hashedPassword,
                Name = userCreateRequest.Name,
            };

            await _userRepository.CreateUser(user);

            return new UserResponse()
            {
                Id = user.Id,
                Email = user.Email,
                Name = user.Name
            };
        }

    }
}
