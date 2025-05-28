using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace Barber.Colocho.Transversal.Middleware.Middlewares
{
    public class RequestLoggingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<RequestLoggingMiddleware> _logger;

        public RequestLoggingMiddleware(RequestDelegate next, ILogger<RequestLoggingMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task Invoke(HttpContext context)
        {
            if (context.Request.Path.Value.Contains("api"))
            {
                // URL
                var url = context.Request.Path + context.Request.QueryString;

                // Método HTTP
                var metodo = context.Request.Method;

                // Headers
                var headers = context.Request.Headers;

                _logger.LogInformation("➡️ Request: {Metodo} {Url}", metodo, url);

                foreach (var header in headers)
                {
                    _logger.LogInformation("🔹 Header: {Key} = {Value}", header.Key, header.Value);
                }
            }           
            await _next(context); // Importante: llama al siguiente middleware
        }
    }
}
