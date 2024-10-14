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
        private readonly String _JWTKey;
        private readonly String _JWTIssuer;
        private readonly String _JWTAudience;
        private readonly int _JWTExpirationInMinutes;

        public AuthenticationService(IPasswordService passwordService, IUsersRepository usersRepository) 
        {
            _passwordService = passwordService ?? throw new ArgumentNullException(nameof(passwordService));
            _usersRepository = usersRepository ?? throw new ArgumentNullException(nameof(usersRepository));
            _JWTKey = Environment.GetEnvironmentVariable(EnvironmentVariables.JWTKey) ?? throw new ArgumentNullException(nameof(EnvironmentVariables.JWTKey));
            _JWTIssuer = Environment.GetEnvironmentVariable(EnvironmentVariables.JWTIssuer) ?? throw new ArgumentNullException(nameof(EnvironmentVariables.JWTIssuer));
            _JWTAudience = Environment.GetEnvironmentVariable(EnvironmentVariables.JWTAudience) ?? throw new ArgumentNullException(nameof(EnvironmentVariables.JWTAudience));
            _JWTExpirationInMinutes = Convert.ToInt32(Environment.GetEnvironmentVariable(EnvironmentVariables.JWTExpirationInMinutes) ?? throw new ArgumentNullException(nameof(EnvironmentVariables.JWTExpirationInMinutes)));
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

           
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_JWTKey));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {
                new Claim("Id", user.Id.ToString()),
                new Claim(ClaimTypes.Role, user.Role.ToString())
            };


            var token = new JwtSecurityToken(
                issuer: _JWTIssuer,
                audience: _JWTAudience,
                claims,
                null,
                expires: DateTime.Now.AddMinutes(_JWTExpirationInMinutes),
                signingCredentials: credentials
                );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
