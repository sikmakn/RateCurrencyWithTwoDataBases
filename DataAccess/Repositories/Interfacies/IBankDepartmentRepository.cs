using System.Threading.Tasks;
using DataAccess.DataBase;

namespace DataAccess.Repositories.Interfacies
{
    public interface IBankDepartmentRepository
    {
        Task<BankDepartment> Find(string address, string name);
    }
}
