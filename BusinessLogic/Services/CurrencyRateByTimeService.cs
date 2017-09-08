
using System.Collections.Generic;
using System.Linq;
using BusinessLogic.Services.Interfacies;
using DataAccess.DataBase;
using DataAccess.Repositories.Interfacies;

namespace BusinessLogic.Services
{
    public class CurrencyRateByTimeService: ICurrencyRateByTimeService
    {
        
        public IQueryable<CurrencyRateByTime> GetAll()
        {
            return new List<CurrencyRateByTime>().AsQueryable();
        }
    }
}
