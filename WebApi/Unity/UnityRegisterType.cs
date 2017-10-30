using BusinessLogic.Helpers;
using BusinessLogic.Helpers.Interfacies;
using BusinessLogic.NbrbApiAccess.Services;
using BusinessLogic.NbrbApiAccess.Services.Interfacies;
using BusinessLogic.RateUpdate;
using BusinessLogic.RateUpdate.Interfacies;
using BusinessLogic.Services;
using BusinessLogic.Services.Interfacies;
using DataAccess.Repositories.Interfacies;
using DataAccess.Repositories.MsSqlRepositories;
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

            container.RegisterType<ICityRepository, CityRepository>(new HierarchicalLifetimeManager());
            container.RegisterType<ICurrencyRepository, CurrencyRepository>(new HierarchicalLifetimeManager());

            container.RegisterType<IParser, Parser>(new HierarchicalLifetimeManager());
            container.RegisterType<IReader, Reader>(new HierarchicalLifetimeManager());
            container.RegisterType<IRateUpdater, RateUpdater>(new HierarchicalLifetimeManager());
            container.RegisterType<ISchedulerParsingJob, SchedulerParsingJob>(new HierarchicalLifetimeManager());

            container.RegisterType<ICurrencyRateByTimeRepository, CurrencyRateByTimeRepository>();
            container.RegisterType<ICurrencyRateByTimeService, CurrencyRateByTimeService>();
            
            container.RegisterType<IRateNbrbService, RateNbrbService>();

            container.RegisterType<IBankDepartmentRepository, BankDepartmentRepository>();

            return container;
        }
    }
}