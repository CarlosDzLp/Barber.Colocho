using Barber.Colocho.Application.Main.Configure;
using Barber.Colocho.Domain.Core.Configure;
using Barber.Colocho.Infraestructure.Main.Configure;
using Barber.Colocho.Infraestructure.Persistence.Configure;
using Barber.Colocho.Transversal.Mapper.Configure;
using Barber.Colocho.Transversal.Resources.Configure;
using Barber.Colocho.Transversal.Swagger.Configure;
using Barber.Colocho.Transversal.Middleware.Configure;
using Barber.Colocho.Transversal.Validations.Configure;

namespace Barber.Colocho.Web.Configure
{
    public static class ConfigureService
    {
        public static IServiceCollection AddServiceConfigure(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddTransversalResources();
            services.AddValidatorService();
            services.AddInfrastructurePersistenceService(configuration);
            services.AddApplicationService(configuration);
            services.AddDomainCoreService(configuration);
            services.AddInfrastructureMainService(configuration);
            services.AddTransversalAutoMapperService();
            //services.AddTransversalWiddlewareService();
            return services;
        }

        public static IApplicationBuilder AddSwaggerConfigure(this IApplicationBuilder app, IServiceCollection services, WebApplication application)
        {
            app.AddSwaggerServiceApp(services, application);
            app.AddTransversalWiddlewareServiceApp();
            return app;
        }
    }
}
