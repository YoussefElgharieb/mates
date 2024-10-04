using Mates.Core.Domain.RepositoryInterfaces;
using Mates.Core.ServiceContracts;
using Mates.Core.Services.ServiceInterfaces;
using Mates.Core.Domain.Entities;
using Mates.Core.DTO.UserDTOs;
using Microsoft.AspNetCore.Http;

namespace Mates.Core.Services
{
    public class UsersService : IUsersService
    {
        private readonly IUsersRepository _userRepository;
        private readonly IPasswordService _passwordService;

        public UsersService(IUsersRepository userRepository, IPasswordService passwordService) 
        { 
            _userRepository = userRepository?? throw new ArgumentNullException("'userRepository' cannot be null");
            _passwordService = passwordService?? throw new ArgumentNullException("'passwordService' cannot be null");
        }

        public async Task<UserResponse?> CreateUser(UserCreateRequest userCreateRequest)
        {
            var userWithSameEmail = await _userRepository.GetUser(userCreateRequest.Email);
            if(userWithSameEmail != null) throw new BadHttpRequestException("A user with the same email already exists");
            
            var hashedPassword = _passwordService.Hash(userCreateRequest.Password);

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
