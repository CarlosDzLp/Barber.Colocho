using Barber.Colocho.Application.DTO.User;
using Barber.Colocho.Transversal.Validations.User;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;

namespace Barber.Colocho.Transversal.Validations.Configure
{
    public static class ConfigureService
    {
        public static IServiceCollection AddValidatorService(this IServiceCollection services)
        {
            services.AddScoped<IValidator<UserApplicationDto>, UserValidator>();
            return services;
        }
    }
}
