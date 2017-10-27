using System.Linq;
using System.Threading.Tasks;
using BusinessLogic.Services.Interfacies;
using DataAccess.ModelsForServices;
using DataAccess.Repositories.Interfacies;

namespace BusinessLogic.Services
{
    public class CurrencyRateByTimeService: ICurrencyRateByTimeService
    {
        private readonly ICurrencyRateByTimeRepository _currencyRateByTimeRepository;

        public CurrencyRateByTimeService(ICurrencyRateByTimeRepository currencyRateByTimeRepository)
        {
            _currencyRateByTimeRepository = currencyRateByTimeRepository;
        }

        public async Task<CurrencyRateByTimeServiceModel> GetById(string id)
        {
            return await _currencyRateByTimeRepository.GetById(id);
        }

        public async Task<IQueryable<CurrencyRateByTimeServiceModel>> GetAllActual()
        {
            var allRates = await _currencyRateByTimeRepository.GetAllActuall();
            return allRates;
        }

        //public IQueryable<CurrencyRateByTime> GetActualByCurrency(string currency)
        //{
        //    var currencyId = _currencyRepository.FindByName(currency).Id;
        //    return _currencyRateByTimeRepository.GetAllActual().Where(x => x.CurrencyId == currencyId);
        //}

        //public double GetBestPurchaseByCurrency(string currency)
        //{
        //    return GetActualByCurrency(currency).Max(x => x.Purchase);
        //}

        //public double GetBestSaleByCurrency(string currency)
        //{
        //    return GetActualByCurrency(currency).Min(x => x.Sale);
        //}

        //public IQueryable<CurrencyRateByTime> GetBestByPurchaseByCurrecny(string currency)
        //{
        //    var actuals = GetActualByCurrency(currency);
        //    var bestPurchase = actuals.Max(x => x.Purchase);
        //    return actuals.Where(x => Math.Abs(x.Purchase - bestPurchase) < 0.0001);
        //}

        //public IQueryable<CurrencyRateByTime> GetBestBySaleByCurrency(string currency)
        //{
        //    var actuals = GetActualByCurrency(currency);
        //    var bestsale = actuals.Min(x => x.Sale);
        //    return actuals.Where(x => Math.Abs(x.Sale - bestsale) < 0.0001);
        //}

    }
}
