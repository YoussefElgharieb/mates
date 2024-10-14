using Mates.API.Constants;
using Mates.Core.Domain.RepositoryInterfaces;
using Mates.Core.DTO.UserDTOs;
using Mates.Core.ServiceContracts;
using Mates.Core.Services.ServiceInterfaces;
using Mates.Core.Services;
using Mates.Infrastructure.Repositories;
using Mates.Infrastructure;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Microsoft.EntityFrameworkCore;
using FluentValidation;
using SharpGrip.FluentValidation.AutoValidation.Mvc.Extensions;

namespace Mates.API.StartupExtensions
{
    public static class ConfigureServicesExtension
    {
        public static void ConfigureServices(this IServiceCollection services, IConfiguration configuration)
        {
            // Add services to the container.
            //JWT
            var JWTIssuer = Environment.GetEnvironmentVariable(EnvironmentVariables.JWTIssuer) ?? throw new ArgumentNullException($"'{nameof(EnvironmentVariables.JWTIssuer)}' environment variable is missing or empty"); ;
            var JWTAudience = Environment.GetEnvironmentVariable(EnvironmentVariables.JWTAudience) ?? throw new ArgumentNullException($"'{nameof(EnvironmentVariables.JWTAudience)}' environment variable is missing or empty");
            var JWTKey = Environment.GetEnvironmentVariable(EnvironmentVariables.JWTKey) ?? throw new ArgumentNullException($"'{nameof(EnvironmentVariables.JWTKey)}' environment variable is missing or empty");

            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    options.TokenValidationParameters = new()
                    {
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidateLifetime = true,
                        ValidateIssuerSigningKey = true,
                        ValidIssuer = JWTIssuer,
                        ValidAudience = JWTAudience,
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes("hHrkctx5NsPvtJFK2mupkVj4KLIQ95HL"))
                    };
                });

            services.AddControllers();

            services.AddDbContext<ApplicationDbContext>(options =>
            {
                options.UseNpgsql(Environment.GetEnvironmentVariable(EnvironmentVariables.DBConnectionString));
            });

            services.AddScoped<IUsersService, UsersService>();
            services.AddScoped<IRelationshipsService, RelationshipsService>();
            services.AddScoped<IAuthenticationService, AuthenticationService>();

            services.AddScoped<IUserIdProvider, UserIdProvider>();

            services.AddSingleton<IPasswordService, PasswordService>();
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

            services.AddScoped<IUsersRepository, UsersRepository>();
            services.AddScoped<IRelationshipsRepository, RelationshipsRepository>();

            //FLuentValidators
            services.AddValidatorsFromAssemblyContaining<CreateUserRequestValidator>();
            services.AddFluentValidationAutoValidation();

        }
    }
}
