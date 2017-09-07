using System.Web.Hosting;
using BusinessLogic.RateUpdate.Interfacies;
using FluentScheduler;
using WebApi.FluentScheduler.Interfacies;

namespace WebApi.FluentScheduler
{
    public class SchedulerParsingJob: IJob, IRegisteredObject, ISchedulerParsingJob
    {
        private readonly object _lock = new object();

        private bool _shuttingDown;

        private readonly IRateUpdater _rateUpdater;

        public SchedulerParsingJob(IRateUpdater rateUpdater)
        {
            // Register this job with the hosting environment.
            // Allows for a more graceful stop of the job, in the case of IIS shutting down.
            _rateUpdater = rateUpdater;
            HostingEnvironment.RegisterObject(this);
        }

        public void Execute()
        {
            lock (_lock)
            {
                if (_shuttingDown)
                    return;

                // Do work, son!
                _rateUpdater.Update();
            }
        }

        public void Stop(bool immediate)
        {
            // Locking here will wait for the lock in Execute to be released until this code can continue.
            lock (_lock)
            {
                _shuttingDown = true;
            }

            HostingEnvironment.UnregisterObject(this);
        }
    }
}