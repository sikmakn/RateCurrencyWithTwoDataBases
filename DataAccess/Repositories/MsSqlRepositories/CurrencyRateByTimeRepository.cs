using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using DataAccess.DataBase;
using DataAccess.ModelsForServices;
using DataAccess.Repositories.Interfacies;
using DataAccess.UnitOfWork;

namespace DataAccess.Repositories.MsSqlRepositories
{
    public class CurrencyRateByTimeRepository: ICurrencyRateByTimeRepository
    {
        private readonly DbSet<CurrencyRateByTime> _currencyRateByTimes;

        public CurrencyRateByTimeRepository(IUnitOfWork unitOfWork)
        {
            _currencyRateByTimes = unitOfWork.Context.Set<CurrencyRateByTime>();
        }

        public async Task<IQueryable<CurrencyRateByTimeServiceModel>> GetAll()
        {
            var currencyServiceModelList = new List<CurrencyRateByTimeServiceModel>();
            await _currencyRateByTimes.ForEachAsync(x =>
                currencyServiceModelList.Add(Mapper.Map<CurrencyRateByTime, CurrencyRateByTimeServiceModel>(x)));
            return currencyServiceModelList.AsQueryable();
        }
        public async Task<CurrencyRateByTimeServiceModel> GetById(string id)
        {
            var flag = int.TryParse(id, out var idInt);

            if (!flag)
                return null;

            var currencyRate = await _currencyRateByTimes.FindAsync(idInt);
            return Mapper.Map<CurrencyRateByTime, CurrencyRateByTimeServiceModel>(currencyRate);
        }

        public async Task<IQueryable<CurrencyRateByTimeServiceModel>> GetAllActuall()
        {
            var span = new TimeSpan(4, 0, 0).TotalHours;
            var now = DateTime.UtcNow;
            var currencyResult = _currencyRateByTimes.Where(x =>
                DbFunctions.DiffMonths(now, x.DateTime) == 0
                && DbFunctions.DiffYears(now, x.DateTime) == 0
                && DbFunctions.DiffDays(now, x.DateTime) == 0
                & DbFunctions.DiffHours(x.DateTime, now) < span);

            var currencyRateServiceModel = new List<CurrencyRateByTimeServiceModel>();
            await currencyResult.ForEachAsync(x =>
                currencyRateServiceModel.Add(Mapper.Map<CurrencyRateByTime, CurrencyRateByTimeServiceModel>(x)));
            return currencyRateServiceModel.AsQueryable();
        }

        public void BulkAdd(IEnumerable<CurrencyRateByTimeServiceModel> currencyRateByTimeServiceModels)
        {
            var currenciesByTimes =
                Mapper.Map<IEnumerable<CurrencyRateByTimeServiceModel>, IEnumerable<CurrencyRateByTime>>(currencyRateByTimeServiceModels);

            _currencyRateByTimes.AddRange(currenciesByTimes);
        }

        public void Add(CurrencyRateByTimeServiceModel currencyRateServiceModel)
        {
            var currencyRate = Mapper.Map<CurrencyRateByTimeServiceModel, CurrencyRateByTime>(currencyRateServiceModel);

            _currencyRateByTimes.Add(currencyRate);
;
        }
    }
}
