using System.Linq;
using System.Threading.Tasks;
using DataAccess.ModelsForServices;

namespace BusinessLogic.Services.Interfacies
{
    public interface ICurrencyRateByTimeService
    {
        Task<IQueryable<CurrencyRateByTimeServiceModel>> GetAllActual();
        //IQueryable<CurrencyRateByTime> GetActualByCurrency(string currency);
        //double GetBestPurchaseByCurrency(string currency);
        //double GetBestSaleByCurrency(string currency);
        //IQueryable<CurrencyRateByTime> GetBestByPurchaseByCurrecny(string currency);
        //IQueryable<CurrencyRateByTime> GetBestBySaleByCurrency(string currency);
        Task<CurrencyRateByTimeServiceModel> GetById(string id);
    }
}
