using Mates.Core.DTO.RelationshipDTOs;
using Mates.Core.ServiceContracts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Mates.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RelationshipsController : ControllerBase
    {
        private readonly IRelationshipsService _relationshipsService;
        public RelationshipsController(IRelationshipsService relationshipsService) 
        { 
            _relationshipsService = relationshipsService?? throw new ArgumentNullException(nameof(relationshipsService));
        }

        [HttpPost]
        [Authorize(Roles = "User")]
        public async Task<ActionResult> Post([FromBody] CreateRelationshipRequest relationshipCreateRequest)
        {
            await _relationshipsService.CreateRelationshipAsync(relationshipCreateRequest);
            return NoContent();
        }
    }
}
