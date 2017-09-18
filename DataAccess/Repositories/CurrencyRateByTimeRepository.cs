using System;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using DataAccess.DataBase;
using DataAccess.Repositories.Interfacies;
using DataAccess.UnitOfWork;

namespace DataAccess.Repositories
{
    public class CurrencyRateByTimeRepository: ICurrencyRateByTimeRepository
    {
        private readonly DbSet<CurrencyRateByTime> _currencyRateByTimes;

        public CurrencyRateByTimeRepository(IUnitOfWork unitOfWork)
        {
            _currencyRateByTimes = unitOfWork.Context.Set<CurrencyRateByTime>();
        }

        public IQueryable<CurrencyRateByTime> GetAllActual()
        {
            var span = new TimeSpan(4, 0, 0).TotalHours;
            var now = DateTime.UtcNow;
            var result = _currencyRateByTimes.Where(x => DbFunctions.DiffDays(now, x.DateTime) == 0 &&
            DbFunctions.DiffHours(x.DateTime, now) < span);
            return result;
        }
        public async Task<CurrencyRateByTime> GetById(int id)
        {
            return await _currencyRateByTimes.FindAsync(id);
        }
    }
}
