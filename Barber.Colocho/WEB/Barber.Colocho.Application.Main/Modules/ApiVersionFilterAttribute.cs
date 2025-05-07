using Barber.Colocho.Application.Interface.Version;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.DependencyInjection;

namespace Barber.Colocho.Application.Main.Modules
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
    public class ApiVersionFilterAttribute : ActionFilterAttribute
    {
        public override async Task OnActionExecutionAsync(Microsoft.AspNetCore.Mvc.Filters.ActionExecutingContext context, ActionExecutionDelegate next)
        {
            var versionApi = context.HttpContext.RequestServices.GetService<IVersionApplication>();
            string apiVersion = context.HttpContext.Request.Headers["Api-Version"].ToString();
            if (string.IsNullOrEmpty(apiVersion))
            {
                context.Result = new BadRequestObjectResult("La versión de la API no está especificada.");
                await context.Result.ExecuteResultAsync(context);
            }
            else
            {
                var version = await versionApi.LastVersion();
                // Verificar si la versión solicitada es compatible
                if (version == null)
                {
                    context.Result = new NotFoundObjectResult($"La versión {apiVersion} de la API ya no está disponible.");
                    await context.Result.ExecuteResultAsync(context);
                }
                else
                {
                    decimal vrs = Convert.ToDecimal(apiVersion);
                    if (version.Result == null || version.Result.VersionApi != vrs)
                    {
                        context.Result = new NotFoundObjectResult($"La versión {apiVersion} de la API ya no está disponible.");
                        await context.Result.ExecuteResultAsync(context);
                    }
                    else
                    {
                        await base.OnActionExecutionAsync(context, next);
                    }
                }
            }
        }
    }
}
