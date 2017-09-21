using System.Threading.Tasks;
using DataAccess.DataBase;

namespace DataAccess.Repositories.Interfacies
{
    public interface IBankRepository
    {
        Bank Add(Bank bank);
        Task<Bank> FindByNameAsync(string name);
    }
}
