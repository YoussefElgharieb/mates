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
        private readonly String _jwtKey;
        private readonly String _jwtIssuer;
        private readonly String _jwtAudience;
        private readonly int _jwtExpirationInMinutes;
        private readonly JwtSecurityTokenHandler _handler;

        public AuthenticationService(IPasswordService passwordService, IUsersRepository usersRepository) 
        {
            _passwordService = passwordService ?? throw new ArgumentNullException(nameof(passwordService));
            _usersRepository = usersRepository ?? throw new ArgumentNullException(nameof(usersRepository));
            _jwtKey = Environment.GetEnvironmentVariable(EnvironmentVariables.JwtKey) ?? throw new ArgumentNullException(nameof(EnvironmentVariables.JwtKey));
            _jwtIssuer = Environment.GetEnvironmentVariable(EnvironmentVariables.JwtIssuer) ?? throw new ArgumentNullException(nameof(EnvironmentVariables.JwtIssuer));
            _jwtAudience = Environment.GetEnvironmentVariable(EnvironmentVariables.JwtAudience) ?? throw new ArgumentNullException(nameof(EnvironmentVariables.JwtAudience));
            _jwtExpirationInMinutes = Convert.ToInt32(Environment.GetEnvironmentVariable(EnvironmentVariables.JwtExpirationInMinutes) ?? throw new ArgumentNullException(nameof(EnvironmentVariables.JwtExpirationInMinutes)));
            _handler = new JwtSecurityTokenHandler();
        }
        public async Task<string> LoginUserAsync(LoginRequest loginRequest)
        {
            var user = await _usersRepository.GetUserByEmailAsync(loginRequest.Email);
            if (user == null)
            {
                throw new BadHttpRequestException($"'{nameof(loginRequest.Email)}' is incorrect");
            }

            var result = _passwordService.Verify(loginRequest.Password, user.Password);
            if (!result)
            {
                throw new BadHttpRequestException($"'{nameof(loginRequest.Password)}' is incorrect");
            }

           
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtKey));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {
                new Claim("Id", user.Id.ToString()),
                new Claim(ClaimTypes.Role, user.Role.ToString())
            };


            var token = new JwtSecurityToken(
                issuer: _jwtIssuer,
                audience: _jwtAudience,
                claims,
                null,
                expires: DateTime.Now.AddMinutes(_jwtExpirationInMinutes),
                signingCredentials: credentials
                );

            return _handler.WriteToken(token);
        }
    }
}
