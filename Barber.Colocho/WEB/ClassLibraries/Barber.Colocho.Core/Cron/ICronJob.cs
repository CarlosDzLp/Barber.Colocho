namespace Barber.Colocho.Core.Cron
{
    public interface ICronJob
    {
        Task Run(CancellationToken token = default);
    }
}
