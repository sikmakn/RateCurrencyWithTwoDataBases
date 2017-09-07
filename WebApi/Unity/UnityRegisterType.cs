using BusinessLogic.RateUpdate;
using BusinessLogic.RateUpdate.Interfacies;
using DataAccess.DataBase;
using DataAccess.Repositories;
using DataAccess.Repositories.Interfacies;
using DataAccess.UnitOfWork;
using Microsoft.Practices.Unity;
using WebApi.FluentScheduler;
using WebApi.FluentScheduler.Interfacies;

namespace WebApi.Unity
{
    public static class UnityRegisterType
    {
        public static UnityContainer RegisterType()
        {
            var container = new UnityContainer();
            container.RegisterType<IUnitOfWork, UnitOfWork>(new HierarchicalLifetimeManager());
            container.RegisterType<IBankDepartmentRepository, BankDepartmentRepository>();
            container.RegisterType<IDictionaryRepository<City>, DictionaryRepository<City>>();
            container.RegisterType<IDictionaryRepository<Currency>, DictionaryRepository<Currency>>();
            container.RegisterType<IParser, Parser>();
            container.RegisterType<IReader, Reader>();
            container.RegisterType<IRateUpdater, RateUpdater>();
            container.RegisterType<ISchedulerParsingJob, SchedulerParsingJob>();
            return container;
        }
    }
}