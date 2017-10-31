using System.Threading.Tasks;
using DataAccess.ModelsForServices;

namespace DataAccess.Repositories.Interfacies
{
    public interface IBankDepartmentRepository
    {
        BankDepartmentServiceModel FindByNameAndAddress(string departmentName, string address);

        Task<string> Add(BankDepartmentServiceModel departmentServiceModel);
    }
}
