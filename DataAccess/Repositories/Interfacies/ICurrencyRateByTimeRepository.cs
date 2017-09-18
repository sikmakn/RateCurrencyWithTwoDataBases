using System.Linq;
using System.Threading.Tasks;
using DataAccess.DataBase;

namespace DataAccess.Repositories.Interfacies
{
    public interface ICurrencyRateByTimeRepository
    {
        IQueryable<CurrencyRateByTime> GetAllActual();
        Task<CurrencyRateByTime> GetById(int id);
    }
}
