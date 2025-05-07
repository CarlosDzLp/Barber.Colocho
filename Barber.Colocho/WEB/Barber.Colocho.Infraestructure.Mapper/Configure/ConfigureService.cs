using Barber.Colocho.Transversal.Mapper.Response;
using Microsoft.Extensions.DependencyInjection;

namespace Barber.Colocho.Transversal.Mapper.Configure
{
    public static class ConfigureService
    {
        public static IServiceCollection AddTransversalAutoMapperService(this IServiceCollection services)
        {
            services.AddAutoMapper((e) =>
            {
                //e.AddProfile<MapperProfile>();
                e.AddProfile<ResponseProfile>();
            });
            return services;
        }
    }
}
