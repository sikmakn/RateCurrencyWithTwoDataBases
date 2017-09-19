using System;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using BusinessLogic.Services.Interfacies;
using DataAccess.DataBase;
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

        public async Task<CurrencyRateByTime> GetById(int id)
        {
            return await _currencyRateByTimeRepository.GetById(id);
        }

        public IQueryable<CurrencyRateByTime> GetAllActual()
        {
            var allRates = _currencyRateByTimeRepository.GetAll();
            var span = new TimeSpan(4, 0, 0).TotalHours;
            var now = DateTime.UtcNow;
            var result = allRates.Where(x =>
                   DbFunctions.DiffMonths(now, x.DateTime) == 0
                && DbFunctions.DiffYears(now, x.DateTime) == 0
                && DbFunctions.DiffDays(now, x.DateTime) == 0
                && DbFunctions.DiffHours(x.DateTime, now) < span);
            return result;
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
