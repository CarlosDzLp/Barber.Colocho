using Microsoft.Extensions.DependencyInjection;

namespace Barber.Colocho.Transversal.Resources.Configure
{
    public static class ConfigureService
    {
        public static IServiceCollection AddTransversalResources(this IServiceCollection services)
        {
            services.AddScoped<BarberResources>();
            return services;
        }
    }
}
