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

        public IQueryable<CurrencyRateByTime> GetAll()
        {
            return _currencyRateByTimes;
        }
        public async Task<CurrencyRateByTime> GetById(int id)
        {
            return await _currencyRateByTimes.FindAsync(id);
        }
    }
}
