namespace Mates.API.Middleware
{
    public static class InjectUserIdMiddlewareExtensions
    {
        public static IApplicationBuilder UseInjectUserIdMiddleware(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<InjectUserIdMiddleware>();
        }
    }
}
