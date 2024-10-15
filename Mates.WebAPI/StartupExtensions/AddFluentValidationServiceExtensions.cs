using FluentValidation;
using Mates.Core.DTO.UserDTOs;
using SharpGrip.FluentValidation.AutoValidation.Mvc.Extensions;

namespace Mates.API.StartupExtensions
{
    public static class AddFluentValidationServiceExtensions
    {
        public static void AddFluentValidationService(this IServiceCollection services)
        {
            //FLuentValidators
            services.AddValidatorsFromAssemblyContaining<CreateUserRequestValidator>();
            services.AddFluentValidationAutoValidation();
        }
    }
}
