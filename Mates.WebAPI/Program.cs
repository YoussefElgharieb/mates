using FluentValidation;
using Mates.API.Constants;
using Mates.API.Middleware;
using Mates.API.StartupExtensions;
using Mates.Core.Domain.RepositoryInterfaces;
using Mates.Core.DTO.UserDTOs;
using Mates.Core.ServiceContracts;
using Mates.Core.Services;
using Mates.Core.Services.ServiceInterfaces;
using Mates.Infrastructure;
using Mates.Infrastructure.Repositories;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.IdentityModel.Tokens;
using SharpGrip.FluentValidation.AutoValidation.Mvc.Extensions;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

builder.Services.ConfigureServices(builder.Configuration);

var app = builder.Build();

// Configure the HTTP request pipeline.

app.UseHttpsRedirection();

app.UseAuthentication();

app.UseAuthorization();

app.UseExceptionHandlingMiddleware();

//app.UseInjectUserIdMiddleware();

app.MapControllers();

app.Run();
