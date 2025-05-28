using Barber.Colocho.Transversal.Middleware.Middlewares;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace Barber.Colocho.Transversal.Middleware.Configure
{
    public static class ConfigureService
    {
        //public static IServiceCollection AddTransversalWiddlewareService(this IServiceCollection services)
        //{
        //    services.AddTransient<RequestLoggingMiddleware>();
        //    return services;
        //}



        public static IApplicationBuilder AddTransversalWiddlewareServiceApp(this IApplicationBuilder app)
        {
            app.UseMiddleware<RequestLoggingMiddleware>();
            return app;
        }
    }
}
