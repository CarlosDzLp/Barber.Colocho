using Barber.Colocho.Infraestructure.Main.Repository;
using Barber.Colocho.Infraestructure.Main.Version;
using Barber.Colocho.Infraestructure.Repository.Repository;
using Barber.Colocho.Infraestructure.Repository.Version;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Barber.Colocho.Infraestructure.Main.Configure
{
    public static class ConfigureService
    {
        public static IServiceCollection AddInfrastructureMainService(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
            services.AddScoped<IVersionInfraestructure, VersionInfraestructure>();
            return services;
        }
    }
}
