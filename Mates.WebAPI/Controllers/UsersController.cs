using Mates.Core.ServiceContracts;
using Microsoft.AspNetCore.Http;
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
        IValidator<UserCreateRequest> _validator;
        public UsersController(IUsersService usersService, IValidator<UserCreateRequest> validator)
        {
            _usersService = usersService;
            _validator = validator;
        }

        [HttpPost]
        public async Task<ActionResult<UserResponse?>> Post([FromBody]UserCreateRequest userCreateRequest)
        {
            var results = _validator.Validate(userCreateRequest);

            if (!results.IsValid)
            {
                var errorMessages = results.Errors.Select(e => e.ErrorMessage).ToArray();
                return BadRequest(new { errors = errorMessages });
            }

            return await _usersService.CreateUser(userCreateRequest);
        }
       
    }
}
