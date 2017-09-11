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

        public IQueryable<CurrencyRateByTime> GetAll()
        {
            return _currencyRateByTimes;
        }

        public IQueryable<CurrencyRateByTime> GetAllActual()
        {
            return _currencyRateByTimes.Where(x => DateTime.UtcNow - x.DateTime <= new TimeSpan(4, 0, 0));
        }

        public IQueryable<CurrencyRateByTime> GetByBankDepartment(int departmentId)
        {
            return _currencyRateByTimes.Where(x => x.BankDepartmentId == departmentId);
        }

        public async Task<CurrencyRateByTime> GetById(int id)
        {
            return await _currencyRateByTimes.FindAsync(id);
        }
        
    }
}
