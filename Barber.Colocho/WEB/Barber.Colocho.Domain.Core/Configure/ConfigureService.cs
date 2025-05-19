using Barber.Colocho.Domain.Core.User;
using Barber.Colocho.Domain.Core.Version;
using Barber.Colocho.Domain.Interface.User;
using Barber.Colocho.Domain.Interface.Version;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Barber.Colocho.Domain.Core.Configure
{
    public static class ConfigureService
    {
        public static IServiceCollection AddDomainCoreService(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<IVersionDomain, VersionDomain>();
            services.AddScoped<IUserDomain, UserDomain>();
            return services;
        }
    }
}
