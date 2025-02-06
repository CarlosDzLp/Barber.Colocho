using Barber.Colocho.Repository.Repository;

namespace Barber.Colocho.Core.Error
{
    public class ErrorBL
    {
        private readonly IGenericRepository<Entities.Tables.Error> error;

        public ErrorBL(IGenericRepository<Entities.Tables.Error> error)
        {
            this.error = error;
        }

        public async Task LogError(Exception exception)
        {
            try
            {
                string message = exception.Message;
                string stack = exception.StackTrace;
                string source = exception.Source;
                await error.AddAsync(new Entities.Tables.Error
                {
                    Created = DateTime.UtcNow,
                    IsActive = true,
                    Message = message,
                    Source = source,
                    StackTrace = stack,
                });
            }
            catch (Exception ex)
            {
                await LogError(ex);
            }
        }
    }
}
