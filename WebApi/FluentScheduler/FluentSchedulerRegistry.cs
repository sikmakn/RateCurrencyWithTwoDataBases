using FluentScheduler;
using WebApi.FluentScheduler.Interfacies;

namespace WebApi.FluentScheduler
{
    public class FluentSchedulerRegistry: Registry
    {
        public FluentSchedulerRegistry()
        {
            Schedule<ISchedulerParsingJob>().ToRunNow();
        }
    }
}