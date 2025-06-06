using Cronos;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace Barber.Colocho.Transversal.Worker.Cron
{
    public class CronWorker : BackgroundService
    {
        private readonly ILogger<CronWorker> _logger;
        private readonly CronExpression _cron;
        private readonly TimeZoneInfo _timeZone;

        public CronWorker(ILogger<CronWorker> logger)
        {
            _logger = logger;
            _cron = CronExpression.Parse("0 1 * * *"); // A la 1:00 AM todos los días
            _timeZone = TimeZoneInfo.Local;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                var now = DateTimeOffset.Now;
                var next = _cron.GetNextOccurrence(now, _timeZone);
                if (next.HasValue)
                {
                    var delay = next.Value - now;
                    _logger.LogInformation("⏳ Esperando hasta la siguiente ejecución: {next}", next.Value);
                    if (delay > TimeSpan.Zero)
                        await Task.Delay(delay, stoppingToken);

                    await EjecutarTarea();
                }
            }
        }

        private async Task EjecutarTarea()
        {
            _logger.LogInformation("✅ Tarea ejecutada a la 1 AM: {time}", DateTimeOffset.Now);
            // Aquí va la lógica que quieres ejecutar
            await Task.Delay(1000); // Simulación
        }
    }
}
