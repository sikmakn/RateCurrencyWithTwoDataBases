using DataAccess.DataBase;
using DataAccess.Repositories;
using DataAccess.Repositories.Interfacies;
using DataAccess.UnitOfWork;
using Microsoft.Practices.Unity;

namespace WebApi.Unity
{
    public static class UnityRegisterType
    {
        public static UnityContainer RegisterType()
        {
            var container = new UnityContainer();
            container.RegisterType<IUnitOfWork, UnitOfWork>();
            container.RegisterType<IBankDepartmentRepository, BankDepartmentRepository>();
            container.RegisterType<IDictionaryRepository<City>, DictionaryRepository<City>>();
            container.RegisterType<IDictionaryRepository<Currency>, DictionaryRepository<Currency>>();
            return container;
        }
    }
}