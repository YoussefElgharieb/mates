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
            _usersService = usersService?? throw new ArgumentNullException("'usersService' cannot be null");
        }

        [HttpPost]
        public async Task<ActionResult<UserResponse?>> Post([FromBody]UserCreateRequest userCreateRequest)
        {
            return await _usersService.CreateUser(userCreateRequest);
        }
       
    }
}
