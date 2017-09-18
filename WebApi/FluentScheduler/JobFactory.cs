using FluentScheduler;
using Microsoft.Practices.Unity;

namespace WebApi.FluentScheduler
{
    public class JobFactory: IJobFactory
    {
        private readonly IUnityContainer _container;

        public JobFactory(IUnityContainer container)
        {
            _container = container;
        }

        public IJob GetJobInstance<T>() where T : IJob
        {
            return _container.Resolve<T>();
        }
    }
}