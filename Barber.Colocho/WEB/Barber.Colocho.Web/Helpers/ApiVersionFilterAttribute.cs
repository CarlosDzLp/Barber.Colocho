using Barber.Colocho.Repository.Repository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Barber.Colocho.Web.Helpers
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
    public class ApiVersionFilterAttribute : ActionFilterAttribute
    {
        public override async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            var versionApi = context.HttpContext.RequestServices.GetService<IGenericRepository<Entities.Tables.VersionApi>>();
            // Leer la versión de la API desde el encabezado (por ejemplo, 'Api-Version')
            string apiVersion = context.HttpContext.Request.Headers["Api-Version"].ToString();

            if (string.IsNullOrEmpty(apiVersion))
            {
                context.Result = new BadRequestObjectResult("La versión de la API no está especificada.");
                await context.Result.ExecuteResultAsync(context);
            }
            else
            {
                var version = await versionApi.FindAsync(c => c.IsActive);
                // Verificar si la versión solicitada es compatible
                if (version == null)
                {
                    context.Result = new NotFoundObjectResult($"La versión {apiVersion} de la API ya no está disponible.");
                    await context.Result.ExecuteResultAsync(context);
                }
                else
                {
                    decimal vrs = Convert.ToDecimal(apiVersion);
                    if (version.Version != vrs)
                    {
                        context.Result = new NotFoundObjectResult($"La versión {apiVersion} de la API ya no está disponible.");
                        await context.Result.ExecuteResultAsync(context);
                    }
                    else
                    {
                        await base.OnActionExecutionAsync(context, next);
                        return;
                    }
                }
            }
        }
    }
}
