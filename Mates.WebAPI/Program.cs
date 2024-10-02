using FluentValidation;
using Mates.API.Constants;
using Mates.API.Middleware;
using Mates.Core.Domain.RepositoryContracts;
using Mates.Core.DTO.UserDTOs;
using Mates.Core.ServiceContracts;
using Mates.Core.Services;
using Mates.Core.Services.ServiceInterfaces;
using Mates.Infrastructure;
using Mates.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

builder.Services.AddDbContext<ApplicationDbContext>(options =>
{
    options.UseNpgsql(Environment.GetEnvironmentVariable(EnvironmentVariables.DBConnectionString));
});

builder.Services.AddScoped<IValidator<UserCreateRequest>, UserCreateRequestValidator>();
builder.Services.AddScoped<IUsersService, UsersService>();
builder.Services.AddScoped<IUsersRepository, UsersRepository>();
builder.Services.AddScoped<IPasswordService, PasswordService>();

var app = builder.Build();

// Configure the HTTP request pipeline.

app.UseHttpsRedirection();

app.UseAuthorization();

app.UseExceptionHandlingMiddleware();

app.MapControllers();

app.Run();
