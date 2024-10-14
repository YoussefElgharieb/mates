using Mates.Core.Services.ServiceInterfaces;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;

namespace Mates.Core.Services
{
    public class UserIdProvider : IUserIdProvider
    {
        private readonly IHttpContextAccessor _context;

        public UserIdProvider(IHttpContextAccessor context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public Guid GetUserId()
        {
            var claims = _context.HttpContext.User.Claims;

            var userIdClaim = claims.FirstOrDefault(c => c.Type == "Id")?.Value;

            Guid userId = Guid.Parse(userIdClaim);
            
            return userId;
        }
    }
}
