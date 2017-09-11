
using System.Linq;
using System.Threading.Tasks;
using DataAccess.DataBase;

namespace BusinessLogic.Services.Interfacies
{
    public interface ICurrencyRateByTimeService
    {
        IQueryable<CurrencyRateByTime> GetAllActual();
        IQueryable<CurrencyRateByTime> GetActualByCurrency(string currency);
        double GetBestPurchaseByCurrency(string currency);
        double GetBestSaleByCurrency(string currency);
        IQueryable<CurrencyRateByTime> GetBestByPurchaseByCurrecny(string currency);
        IQueryable<CurrencyRateByTime> GetBestBySaleByCurrency(string currency);
        IQueryable<CurrencyRateByTime> GetHistoryByNbRB();
        Task<CurrencyRateByTime> GetById(int id);
    }
}
