using Mates.Core.Services.ServiceInterfaces;
using Microsoft.AspNetCore.Http;

namespace Mates.Core.Services
{
    public class UserProvider : IUserProvider
    {
        private readonly IHttpContextAccessor _context;

        public UserProvider(IHttpContextAccessor context)
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
