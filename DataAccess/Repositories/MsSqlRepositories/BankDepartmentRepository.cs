using System.Data.Entity;
using System.Linq;
using AutoMapper;
using DataAccess.DataBase;
using DataAccess.ModelsForServices;
using DataAccess.Repositories.Interfacies;
using DataAccess.UnitOfWork;

namespace DataAccess.Repositories.MsSqlRepositories
{
    public class BankDepartmentRepository: IBankDepartmentRepository
    {
        private readonly DbSet<BankDepartment> _departments;

        public BankDepartmentRepository(IUnitOfWork unitOfWork)
        {
            _departments = unitOfWork.Context.BankDepartment;
        }

        public BankDepartmentServiceModel FindByName(string departmentName)
        {
            var department = _departments.FirstOrDefault(x => x.Name == departmentName);

            return Mapper.Map<BankDepartment, BankDepartmentServiceModel>(department);
        }

        public void Add(BankDepartmentServiceModel departmentServiceModel)
        {
            var department = Mapper.Map<BankDepartmentServiceModel, BankDepartment>(departmentServiceModel);
            _departments.Add(department);
        }
    }
}
