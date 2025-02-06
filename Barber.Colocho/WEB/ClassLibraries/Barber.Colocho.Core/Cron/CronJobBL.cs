using Barber.Colocho.DataAccess.Db;
using Microsoft.Extensions.DependencyInjection;

namespace Barber.Colocho.Core.Cron
{
    public class CronJobBL : ICronJob
    {
        private readonly IServiceProvider serviceProvider;

        public CronJobBL(IServiceProvider serviceProvider)
        {
            this.serviceProvider = serviceProvider;
        }
        public Task Run(CancellationToken token = default)
        {
            CreateScope();
            return Task.CompletedTask;
        }

        private void CreateScope()
        {
            ApplicationContext application = null;
            try
            {              
                using (var scope = serviceProvider.CreateScope())
                {
                    application = scope.ServiceProvider.GetRequiredService<ApplicationContext>();
                    Suscription(application);
                }
            }
            catch (Exception ex)
            {
                if (application != null) 
                {
                    application.Error.AddAsync(new Entities.Tables.Error
                    {
                        Created = DateTime.UtcNow,
                        Message = ex.Message,
                        IsActive = true,
                        Source = ex.Source,
                        StackTrace = ex.StackTrace
                    });
                    application.SaveChanges();
                }
            }
        }

        private void Suscription(ApplicationContext applicationContext)
        {
            try
            {
                applicationContext.Error.AddAsync(new Entities.Tables.Error
                {
                    Created = DateTime.UtcNow,
                    Message = $"Se inicia el cronJob {DateTime.UtcNow}",
                    IsActive = true,
                    Source = string.Empty,
                    StackTrace = string.Empty
                });
                applicationContext.SaveChanges();

                var resutl = applicationContext.Suscription.Where(c=>c.IsActive);
                foreach (var item in resutl)
                {
                    var date = DateTime.UtcNow;
                    TimeSpan diferencia = item.FinishSuscription - date;
                    double minutos = diferencia.TotalDays;
                    if (minutos <= 0)
                    {
                        item.IsActive = true;
                        applicationContext.Suscription.Update(item);
                        applicationContext.SaveChanges();
                    }
                }

                applicationContext.Error.AddAsync(new Entities.Tables.Error
                {
                    Created = DateTime.UtcNow,
                    Message = $"Se termina el cronJob {DateTime.UtcNow}",
                    IsActive = true,
                    Source = string.Empty,
                    StackTrace = string.Empty
                });
                applicationContext.SaveChanges();
            }
            catch (Exception ex)
            {
                if (applicationContext != null)
                {
                    applicationContext.Error.AddAsync(new Entities.Tables.Error
                    {
                        Created = DateTime.UtcNow,
                        Message = ex.Message,
                        IsActive = true,
                        Source = ex.Source,
                        StackTrace = ex.StackTrace
                    });
                    applicationContext.SaveChanges();
                }
            }
        }
    }
}
