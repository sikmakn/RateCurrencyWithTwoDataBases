using BusinessLogic.RateUpdate;
using BusinessLogic.RateUpdate.Interfacies;
using BusinessLogic.Services;
using BusinessLogic.Services.Interfacies;
using DataAccess.DataBase;
using DataAccess.Repositories;
using DataAccess.Repositories.Interfacies;
using DataAccess.UnitOfWork;
using Microsoft.Practices.Unity;
using WebApi.Controllers;
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
            container.RegisterType<IBankDepartmentRepository, BankDepartmentRepository>(new HierarchicalLifetimeManager());

            container.RegisterType<IDictionaryRepository<City>, DictionaryRepository<City>>(new HierarchicalLifetimeManager());
            container.RegisterType<IDictionaryRepository<Currency>, DictionaryRepository<Currency>>(new HierarchicalLifetimeManager());

            container.RegisterType<IBankRepository, BankRepository>(new HierarchicalLifetimeManager());

            container.RegisterType<IParser, Parser>(new HierarchicalLifetimeManager());
            container.RegisterType<IReader, Reader>(new HierarchicalLifetimeManager());
            container.RegisterType<IRateUpdater, RateUpdater>(new HierarchicalLifetimeManager());
            container.RegisterType<ISchedulerParsingJob, SchedulerParsingJob>(new HierarchicalLifetimeManager());

            container.RegisterType<ICurrencyRateByTimeRepository, CurrencyRateByTimeRepository>();
            container.RegisterType<ICurrencyRateByTimeService, CurrencyRateByTimeService>();

            return container;
        }
    }
}