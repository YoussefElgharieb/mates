using FluentValidation;
using Mates.API.Constants;
using Mates.API.Middleware;
using Mates.Core.Domain.RepositoryInterfaces;
using Mates.Core.DTO.UserDTOs;
using Mates.Core.ServiceContracts;
using Mates.Core.Services;
using Mates.Core.Services.ServiceInterfaces;
using Mates.Infrastructure;
using Mates.Infrastructure.Repositories;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using SharpGrip.FluentValidation.AutoValidation.Mvc.Extensions;
using System.Text;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

builder.Services.AddDbContext<ApplicationDbContext>(options =>
{
    options.UseNpgsql(Environment.GetEnvironmentVariable(EnvironmentVariables.DBConnectionString));
});

builder.Services.AddScoped<IUsersService, UsersService>();
builder.Services.AddScoped<IRelationshipsService, RelationshipsService>();
builder.Services.AddScoped<IAuthenticationService, AuthenticationService>();

builder.Services.AddSingleton<IPasswordService, PasswordService>();

builder.Services.AddScoped<IUsersRepository, UsersRepository>();
builder.Services.AddScoped<IRelationshipsRepository, RelationshipsRepository>();

//FLuentValidators
builder.Services.AddValidatorsFromAssemblyContaining<CreateUserRequestValidator>();
builder.Services.AddFluentValidationAutoValidation();

//JWT
var JWTIssuer = Environment.GetEnvironmentVariable(EnvironmentVariables.JWTIssuer) ?? throw new ArgumentException($"'{nameof(EnvironmentVariables.JWTIssuer)}' environment variable is missing or empty"); ;
var JWTAudience = Environment.GetEnvironmentVariable(EnvironmentVariables.JWTAudience) ?? throw new ArgumentException($"'{nameof(EnvironmentVariables.JWTAudience)}' environment variable is missing or empty");
var JWTKey = Environment.GetEnvironmentVariable(EnvironmentVariables.JWTKey) ?? throw new ArgumentException($"'{nameof(EnvironmentVariables.JWTKey)}' environment variable is missing or empty");

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = JWTIssuer,
            ValidAudience = JWTAudience,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(JWTKey))
        };
    });

//Swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options => options.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, "api.xml")));

var app = builder.Build();

// Configure the HTTP request pipeline.

app.UseSwagger();

app.UseSwaggerUI();

app.UseHttpsRedirection();

app.UseAuthentication();

app.UseAuthorization();

app.UseExceptionHandlingMiddleware();

app.MapControllers();

app.Run();
