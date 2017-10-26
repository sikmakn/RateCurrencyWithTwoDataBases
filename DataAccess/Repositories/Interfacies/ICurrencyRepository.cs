using System.Collections.Generic;
using System.Threading.Tasks;
using DataAccess.ModelsForServices;

namespace DataAccess.Repositories.Interfacies
{
    public interface ICurrencyRepository
    {
        Task<ICollection<CurrencyServiceModel>> GetAll();
    }
}
