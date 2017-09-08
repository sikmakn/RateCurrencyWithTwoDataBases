
using System.Linq;
using DataAccess.DataBase;

namespace BusinessLogic.Services.Interfacies
{
    public interface ICurrencyRateByTimeService
    {
        IQueryable<CurrencyRateByTime> GetAll();
    }
}
