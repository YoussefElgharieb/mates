using Mates.API.Constants;
using Mates.Core.Domain.RepositoryInterfaces;
using Mates.Core.DTO.AuthenticationDTOs;
using Mates.Core.Services.ServiceInterfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
namespace Mates.Core.Services
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly IPasswordService _passwordService;
        private readonly IUsersRepository _usersRepository;

        public AuthenticationService(IPasswordService passwordService, IUsersRepository usersRepository) 
        {
            _passwordService = passwordService ?? throw new ArgumentNullException(nameof(passwordService));
            _usersRepository = usersRepository ?? throw new ArgumentNullException(nameof(usersRepository));    
        }
        public async Task<string> LoginUserAsync(LoginRequest loginRequest)
        {
            //throw new NotImplementedException();
            var user = await _usersRepository.GetUserByEmailAsync(loginRequest.Email);
            if (user == null)
            {
                throw new BadHttpRequestException($"'{nameof(loginRequest.Email)}' is incorrect");
            }

            var result = _passwordService.Verify(loginRequest.Password, user.Password);
            if (result == false)
            {
                throw new BadHttpRequestException($"'{nameof(loginRequest.Password)}' is incorrect");
            }

            var JWTKey = Environment.GetEnvironmentVariable(EnvironmentVariables.JWTKey) ?? throw new ArgumentNullException($"'{nameof(EnvironmentVariables.JWTKey)}' environment variable is missing or empty");
            var JWTIssuer = Environment.GetEnvironmentVariable(EnvironmentVariables.JWTIssuer) ?? throw new ArgumentNullException($"'{nameof(EnvironmentVariables.JWTIssuer)}' environment variable is missing or empty");
            var JWTAudience = Environment.GetEnvironmentVariable(EnvironmentVariables.JWTAudience) ?? throw new ArgumentNullException($"'{nameof(EnvironmentVariables.JWTAudience)}' environment variable is missing or empty");

            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(JWTKey));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.Id.ToString()),
                new Claim(ClaimTypes.Role, user.Role.ToString())
            };


            var token = new JwtSecurityToken(
                issuer: JWTIssuer,
                audience: JWTAudience,
                claims,
                null,
                expires: DateTime.Now.AddMinutes(15),
                signingCredentials: credentials
                );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
