using System.Linq;
using System.Threading.Tasks;
using DataAccess.DataBase;

namespace DataAccess.Repositories.Interfacies
{
    public interface ICurrencyRateByTimeRepository
    {
        IQueryable<CurrencyRateByTime> GetAllActual();
        IQueryable<CurrencyRateByTime> GetByBankDepartment(int departmentId);
        Task<CurrencyRateByTime> GetById(int id);
        CurrencyRateByTime Add(CurrencyRateByTime currencyRateByTime);
    }
}
