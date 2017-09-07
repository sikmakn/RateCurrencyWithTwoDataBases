
using FluentScheduler;

namespace WebApi.FluentScheduler.Interfacies
{
    public interface ISchedulerParsingJob: IJob
    {
        void Stop(bool immediate);
    }
}
