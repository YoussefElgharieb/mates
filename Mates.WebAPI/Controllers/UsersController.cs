using Mates.Core.ServiceContracts;
using Microsoft.AspNetCore.Mvc;
using Mates.Core.DTO.UserDTOs;
using FluentValidation;

namespace Mates.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        IUsersService _usersService;
        public UsersController(IUsersService usersService)
        {
            _usersService = usersService ?? throw new ArgumentNullException(nameof(usersService));
        }

        /// <summary>
        /// create a user using an email, name, and password.
        /// </summary>
        /// <param name="userCreateRequest"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ActionResult<UserResponse>> Post([FromBody]CreateUserRequest userCreateRequest)
        {
            return await _usersService.CreateUserAsync(userCreateRequest);
        }
    }
}
