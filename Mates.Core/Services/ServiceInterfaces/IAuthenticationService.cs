using Mates.Core.DTO.AuthenticationDTOs;
using Microsoft.AspNetCore.Mvc;

namespace Mates.Core.Services.ServiceInterfaces
{
    public interface IAuthenticationService
    {
        public Task<string> LoginUserAsync(LoginRequest loginRequest);
    }
}
