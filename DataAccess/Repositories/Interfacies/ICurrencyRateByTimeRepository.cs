using System.Linq;
using System.Threading.Tasks;
using DataAccess.DataBase;

namespace DataAccess.Repositories.Interfacies
{
    public interface ICurrencyRateByTimeRepository
    {
        Task<CurrencyRateByTime> GetById(int id);
        IQueryable<CurrencyRateByTime> GetAll();
    }
}
