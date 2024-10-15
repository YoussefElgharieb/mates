using Mates.API.Constants;
using Mates.Infrastructure;
using Microsoft.EntityFrameworkCore;

namespace Mates.API.StartupExtensions
{
    public static class AddDbContextServiceExtension
    {
        public static void AddDbContextService(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<ApplicationDbContext>(options =>
            {
                options.UseNpgsql(Environment.GetEnvironmentVariable(EnvironmentVariables.DBConnectionString));
            });
        }
    }
}
