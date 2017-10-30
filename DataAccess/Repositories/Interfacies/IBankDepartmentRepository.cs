using DataAccess.ModelsForServices;

namespace DataAccess.Repositories.Interfacies
{
    public interface IBankDepartmentRepository
    {
        BankDepartmentServiceModel FindByName(string departmentName);

        void Add(BankDepartmentServiceModel departmentServiceModel);
    }
}
