﻿using Mates.API.Constants;
using Mates.Core.Domain.RepositoryInterfaces;
using Mates.Core.DTO.UserDTOs;
using Mates.Core.ServiceContracts;
using Mates.Core.Services.ServiceInterfaces;
using Mates.Core.Services;
using Mates.Infrastructure.Repositories;
using FluentValidation;
using SharpGrip.FluentValidation.AutoValidation.Mvc.Extensions;

namespace Mates.API.StartupExtensions
{
    public static class AddServicesExtension
    {
        public static void AddServices(this IServiceCollection services)
        {
            services.AddControllers();

            services.AddScoped<IUsersService, UsersService>();
            services.AddScoped<IRelationshipsService, RelationshipsService>();
            services.AddScoped<IAuthenticationService, AuthenticationService>();

            services.AddScoped<IUserProvider, UserProvider>();

            services.AddSingleton<IPasswordService, PasswordService>();
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

            services.AddScoped<IUsersRepository, UsersRepository>();
            services.AddScoped<IRelationshipsRepository, RelationshipsRepository>();
        }
    }
}
