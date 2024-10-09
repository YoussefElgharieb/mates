using Mates.Core.Domain.RepositoryInterfaces;
using Mates.Core.ServiceContracts;
using Mates.Core.Services.ServiceInterfaces;
using Mates.Core.Domain.Entities;
using Mates.Core.DTO.UserDTOs;
using Microsoft.AspNetCore.Http;
using Mates.Core.Domain.Enums;

namespace Mates.Core.Services
{
    public class UsersService : IUsersService
    {
        private readonly IUsersRepository _userRepository;
        private readonly IPasswordService _passwordService;

        public UsersService(IUsersRepository userRepository, IPasswordService passwordService) 
        { 
            _userRepository = userRepository ?? throw new ArgumentNullException(nameof(userRepository));
            _passwordService = passwordService ?? throw new ArgumentNullException(nameof(passwordService));
        }

        public async Task<UserResponse> CreateUserAsync(CreateUserRequest userCreateRequest)
        {
            var userWithSameEmail = await _userRepository.GetUserByEmailAsync(userCreateRequest.Email);
            if(userWithSameEmail != null) throw new BadHttpRequestException("A user with the same email already exists");
            
            var hashedPassword = _passwordService.Hash(userCreateRequest.Password);

            var user = new User() 
            { 
                Id = Guid.NewGuid(), 
                Email = userCreateRequest.Email, 
                Password = hashedPassword,
                Name = userCreateRequest.Name,
                Role = Role.User
            };

            await _userRepository.CreateUserAsync(user);

            return new UserResponse()
            {
                Id = user.Id,
                Email = user.Email,
                Name = user.Name
            };
        }
    }
}
