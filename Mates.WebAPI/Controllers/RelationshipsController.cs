using Mates.Core.Domain.Enums;
using Mates.Core.DTO.RelationshipDTOs;
using Mates.Core.DTO.UserDTOs;
using Mates.Core.ServiceContracts;
using Mates.Core.Services.ServiceInterfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Mates.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RelationshipsController : ControllerBase
    {
        private readonly IRelationshipsService _relationshipsService;
        private readonly IUserIdProvider _userIdProvider;
        public RelationshipsController(IRelationshipsService relationshipsService, IUserIdProvider userIdProvider) 
        { 
            _relationshipsService = relationshipsService?? throw new ArgumentNullException(nameof(relationshipsService));
            _userIdProvider = userIdProvider ?? throw new ArgumentNullException(nameof(userIdProvider));
        }

        [HttpPost]
        [Authorize(Roles = nameof(Role.User))]
        public async Task<ActionResult> Post([FromBody] CreateRelationshipRequest relationshipCreateRequest)
        {
            Guid UserId = _userIdProvider.GetUserId();
            await _relationshipsService.CreateRelationshipAsync(UserId, relationshipCreateRequest);
            return NoContent();
        }

        [HttpGet("friends")]
        [Authorize(Roles = nameof(Role.User))]
        public async Task<ActionResult<List<UserResponse>>> GetFriends()
        {
            Guid UserId = _userIdProvider.GetUserId();
            return await _relationshipsService.GetFriendsAsync(UserId);
        }
    }
}
