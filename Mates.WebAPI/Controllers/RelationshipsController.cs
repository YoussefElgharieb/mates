using Mates.Core.DTO.UserDTOs;
using Mates.Core.DTO.RelationshipDTOs;
using Mates.Core.ServiceContracts;
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
            _relationshipsService = relationshipsService?? throw new ArgumentNullException("'relationshipsService' cannot be null");
        }

        [HttpPost]
        public async Task<ActionResult<RelationshipResponse?>> Post([FromBody] RelationshipCreateRequest relationshipCreateRequest)
        {
            return await _relationshipsService.CreateRelationshipAsync(relationshipCreateRequest);
        }

        [HttpGet]
        public async Task<ActionResult<List<RelationshipResponse>>> GetFriends()
    }
}
