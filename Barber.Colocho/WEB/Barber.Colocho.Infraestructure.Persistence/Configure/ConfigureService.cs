using Barber.Colocho.Infraestructure.Persistence.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Barber.Colocho.Infraestructure.Persistence.Configure
{
    public static class ConfigureService
    {
        public static IServiceCollection AddInfrastructurePersistenceService(this IServiceCollection services, IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
            services.AddDbContext<ApplicationDbContext>(options =>
            {
                options.UseSqlServer(connectionString, action => action.UseNetTopologySuite());
            });
            return services;
        }
    }
}
