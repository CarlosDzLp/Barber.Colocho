using Barber.Colocho.Application.Interface.Version;
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
            return services;
        }
    }
}
