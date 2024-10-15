using Mates.Core.ServiceContracts;
using Microsoft.AspNetCore.Mvc;
using Mates.Core.DTO.UserDTOs;
using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Mates.Core.Domain.Enums;
using Mates.Core.Services.ServiceInterfaces;

namespace Mates.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [AllowAnonymous]
    public class UsersController : ControllerBase
    {
        IUsersService _usersService;
        public UsersController(IUsersService usersService)
        {
            _usersService = usersService ?? throw new ArgumentNullException(nameof(usersService));
        }

        [HttpPost]
        [Authorize(Roles = nameof(Role.Admin))]
        public async Task<ActionResult<UserResponse>> Post([FromBody] CreateUserRequest userCreateRequest)
        {
            return await _usersService.CreateUserAsync(userCreateRequest);
        }
    }
}
