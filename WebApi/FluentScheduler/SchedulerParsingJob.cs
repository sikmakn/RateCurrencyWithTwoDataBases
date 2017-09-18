using System.Web.Hosting;
using BusinessLogic.RateUpdate.Interfacies;
using WebApi.FluentScheduler.Interfacies;

namespace WebApi.FluentScheduler
{
    public class SchedulerParsingJob: IRegisteredObject, ISchedulerParsingJob
    {
        private readonly object _lock = new object();

        private bool _shuttingDown;

        private readonly IRateUpdater _rateUpdater;

        public SchedulerParsingJob(IRateUpdater rateUpdater)
        {
            _rateUpdater = rateUpdater;
            HostingEnvironment.RegisterObject(this);
        }

        public void Execute()
        {
            lock (_lock)
            {
                if (_shuttingDown)
                    return;
                
                _rateUpdater.Update().GetAwaiter().GetResult();
            }
        }

        public void Stop(bool immediate)
        {
            lock (_lock)
            {
                _shuttingDown = true;
            }

            HostingEnvironment.UnregisterObject(this);
        }
    }
}