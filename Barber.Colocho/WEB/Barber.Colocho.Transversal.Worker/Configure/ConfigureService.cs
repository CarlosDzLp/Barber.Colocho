using Barber.Colocho.Transversal.Worker.Cron;
using Microsoft.Extensions.DependencyInjection;

namespace Barber.Colocho.Transversal.Worker.Configure
{
    public static class ConfigureService
    {
        public static IServiceCollection AddCronJobService(this IServiceCollection services)
        {
            services.AddHostedService<CronWorker>();
            return services;
        }
    }
}
