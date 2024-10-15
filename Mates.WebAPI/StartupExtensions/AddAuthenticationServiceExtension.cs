using Mates.API.Constants;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace Mates.API.StartupExtensions
{
    public static class AddAuthenticationServiceExtension
    {
        public static void AddAuthenticationService(this IServiceCollection services)
        {
            // Add services to the container.
            //JWT
            var JwtIssuer = Environment.GetEnvironmentVariable(EnvironmentVariables.JwtIssuer) ?? throw new ArgumentNullException(nameof(EnvironmentVariables.JwtIssuer)); ;
            var JwtAudience = Environment.GetEnvironmentVariable(EnvironmentVariables.JwtAudience) ?? throw new ArgumentNullException(nameof(EnvironmentVariables.JwtAudience));
            var JwtKey = Environment.GetEnvironmentVariable(EnvironmentVariables.JwtKey) ?? throw new ArgumentNullException(nameof(EnvironmentVariables.JwtKey));

            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    options.TokenValidationParameters = new()
                    {
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidateLifetime = true,
                        ValidateIssuerSigningKey = true,
                        ValidIssuer = JwtIssuer,
                        ValidAudience = JwtAudience,
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes("hHrkctx5NsPvtJFK2mupkVj4KLIQ95HL"))
                    };
                });

        }
    }
}
