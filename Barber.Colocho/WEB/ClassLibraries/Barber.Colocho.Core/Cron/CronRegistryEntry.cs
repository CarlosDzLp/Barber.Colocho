using NCrontab;

namespace Barber.Colocho.Core.Cron
{
    public sealed record CronRegistryEntry(Type Type, CrontabSchedule CrontabSchedule);
}
