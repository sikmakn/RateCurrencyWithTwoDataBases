using FluentScheduler;
using WebApi.FluentScheduler.Interfacies;

namespace WebApi.FluentScheduler
{
    public class FluentSchedulerRegistry: Registry
    {
        public FluentSchedulerRegistry()
        {
            var schedule = Schedule<ISchedulerParsingJob>();
            schedule.ToRunNow().AndEvery(4).Hours();
        }
    }
}