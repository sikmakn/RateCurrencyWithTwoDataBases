using System.Collections.Generic;
using System.Threading.Tasks;
using DataAccess.DataBase;

namespace DataAccess.Repositories.Interfacies
{
    public interface IBankDepartmentRepository
    {
        Task<BankDepartment> GetById(int id);
        Task<BankDepartment> Find(string address, string name);
        Task AddOrUpdatesRange(ICollection<BankDepartment> departments);

    }
}
