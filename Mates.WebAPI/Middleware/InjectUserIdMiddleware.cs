using Mates.Core.DTO.RelationshipDTOs;
using System.Security.Claims;
using System.Text.Json;
using System.Text;
using System.IdentityModel.Tokens.Jwt;

namespace Mates.API.Middleware
{
    public class InjectUserIdMiddleware
    {
        private readonly RequestDelegate _next;

        public InjectUserIdMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            if (context.Request.Method == HttpMethods.Post && context.Request.Path.StartsWithSegments("/api/relationships"))
            {
                var claims = context.User.Claims;

                var userIdClaim = claims.FirstOrDefault(c => c.Type == "Id")?.Value;


                if (string.IsNullOrEmpty(userIdClaim) || !Guid.TryParse(userIdClaim, out var userId))
                {
                    context.Response.StatusCode = StatusCodes.Status401Unauthorized;
                    await context.Response.WriteAsync("User ID is invalid or missing.");
                    return;
                }

                context.Request.EnableBuffering(); 
                var reader = new StreamReader(context.Request.Body);
                var body = await reader.ReadToEndAsync();

                context.Request.Body.Position = 0;


                var jsonRequest = JsonSerializer.Deserialize<CreateRelationshipRequest>(body, new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                });

                if (jsonRequest != null)
                {
                    jsonRequest.UserId = userId;

                    var modifiedBody = JsonSerializer.Serialize(jsonRequest);

                    var modifiedBodyBytes = Encoding.UTF8.GetBytes(modifiedBody);

                    context.Request.Body = new MemoryStream(modifiedBodyBytes);
                }
            }

            await _next(context);
        }
    }
}
