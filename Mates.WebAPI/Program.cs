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
using Microsoft.EntityFrameworkCore;
using SharpGrip.FluentValidation.AutoValidation.Mvc.Extensions;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

builder.Services.AddDbContext<ApplicationDbContext>(options =>
{
    options.UseNpgsql(Environment.GetEnvironmentVariable(EnvironmentVariables.DBConnectionString));
});

builder.Services.AddScoped<IUsersService, UsersService>();
builder.Services.AddScoped<IRelationshipsService, RelationshipsService>();
builder.Services.AddScoped<IPasswordService, PasswordService>();

builder.Services.AddScoped<IUsersRepository, UsersRepository>();
builder.Services.AddScoped<IRelationshipsRepository, RelationshipsRepository>();

// FLuentValidators
builder.Services.AddValidatorsFromAssemblyContaining<UserCreateRequestValidator>();
builder.Services.AddFluentValidationAutoValidation();

var app = builder.Build();

// Configure the HTTP request pipeline.

app.UseHttpsRedirection();

app.UseAuthorization();

app.UseExceptionHandlingMiddleware();

app.MapControllers();

app.Run();
