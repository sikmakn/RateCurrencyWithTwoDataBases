using System.Threading.Tasks;
using DataAccess.ModelsForServices;

namespace DataAccess.Repositories.Interfacies
{
    public interface IBankRepository
    {
        BankServiceModel Add(BankServiceModel bank);
        Task<BankServiceModel> FindByNameAsync(string name);
    }
}
