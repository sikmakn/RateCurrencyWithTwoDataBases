using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
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

        public BankDepartmentServiceModel FindByNameAndAddress(string departmentName, string address)
        {
            var department = _departments.FirstOrDefault(x => x.Name == departmentName && x.Address == address) ??
                             _departments.Local.FirstOrDefault(x => x.Name == departmentName && x.Address == address);

            return Mapper.Map<BankDepartment, BankDepartmentServiceModel>(department);
        }

        public async Task<string> Add(BankDepartmentServiceModel departmentServiceModel)
        {
            var department = Mapper.Map<BankDepartmentServiceModel, BankDepartment>(departmentServiceModel);
            var lastId = 0;
            await Task.Run(() =>
            {
                var savedMax = 0;
                var localMax = 0;
                if (_departments.Any())
                {
                    savedMax = _departments.Max(x => x.Id);
                }
                if (_departments.Local.Any())
                {
                    localMax = _departments.Local.Max(x => x.Id);
                }

                lastId = savedMax > localMax ? savedMax : localMax;
                var newId = lastId + 1;

                department.Id = newId;
                _departments.Add(department);

                departmentServiceModel.Id = newId.ToString();
            });

            return departmentServiceModel.Id;
        }
    }
}
