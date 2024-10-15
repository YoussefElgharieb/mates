using Mates.Core.Domain.Enums;
using Mates.Core.DTO.RelationshipDTOs;
using Mates.Core.ServiceContracts;
using Mates.Core.Services;
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
        public RelationshipsController(IRelationshipsService relationshipsService, IUserProvider userIdProvider) 
        { 
            _relationshipsService = relationshipsService?? throw new ArgumentNullException(nameof(relationshipsService));
        }

        [HttpPost]
        [Authorize(Roles = nameof(Role.User))]
        public async Task<ActionResult> Post([FromBody] CreateRelationshipRequest relationshipCreateRequest)
        {
            await _relationshipsService.CreateRelationshipAsync(relationshipCreateRequest);
            return NoContent();
        }
    }
}
