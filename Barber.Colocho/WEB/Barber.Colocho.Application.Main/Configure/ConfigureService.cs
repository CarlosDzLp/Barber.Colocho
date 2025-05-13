using Barber.Colocho.Application.Interface.User;
using Barber.Colocho.Application.Interface.Version;
using Barber.Colocho.Application.Main.User;
using Barber.Colocho.Application.Main.Version;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Barber.Colocho.Application.Main.Configure
{
    public static class ConfigureService
    {
        public static IServiceCollection AddApplicationService(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<IVersionApplication, VersionApplication>();
            services.AddScoped<IUserApplication, UserApplication>();
            return services;
        }
    }
}
