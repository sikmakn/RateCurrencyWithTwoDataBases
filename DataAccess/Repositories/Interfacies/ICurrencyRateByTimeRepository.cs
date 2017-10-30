using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DataAccess.ModelsForServices;

namespace DataAccess.Repositories.Interfacies
{
    public interface ICurrencyRateByTimeRepository
    {
        Task<CurrencyRateByTimeServiceModel> GetById(string id);

        Task<IQueryable<CurrencyRateByTimeServiceModel>> GetAll();

        Task<IQueryable<CurrencyRateByTimeServiceModel>> GetAllActuall();

        void BulkAdd(IEnumerable<CurrencyRateByTimeServiceModel> currencyRateByTimeServiceModels);

        void Add(CurrencyRateByTimeServiceModel currencyRateServiceModel);
    }
}
