using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.OData;
using BusinessLogic.Services.Interfacies;
using DataAccess.DataBase;
using DataAccess.ModelsForServices;

namespace WebApi.Controllers
{

    public class CurrencyRateByTimeController : ODataController
    {
        private readonly ICurrencyRateByTimeService _currencyRateByTimeService;

        public CurrencyRateByTimeController(ICurrencyRateByTimeService currencyRateByTimeService)
        {
            _currencyRateByTimeService = currencyRateByTimeService;
        }

        // GET: odata/CurrencyRateByTimes
        [EnableQuery]
        public async Task<IQueryable<CurrencyRateByTimeServiceModel>> GetCurrencyRateByTime()
        {
            return await _currencyRateByTimeService.GetAllActual();
        }

        // GET: odata/CurrencyRateByTimes(5)
        [EnableQuery]
        public async Task<SingleResult<CurrencyRateByTimeServiceModel>> GetCurrencyRateByTime([FromODataUri] string key)
        {
            var currencyRateByTime = await _currencyRateByTimeService.GetById(key);
            var query = new List<CurrencyRateByTimeServiceModel> {currencyRateByTime};
            return SingleResult.Create(query.AsQueryable());
        }

        [EnableQuery]
        public async Task<SingleResult<BankDepartmentServiceModel>> GetBankDepartment([FromODataUri] string key)
        {
            var currencyRateByTime = await _currencyRateByTimeService.GetById(key);
            var department = currencyRateByTime?.BankDepartment;
            var query = new List<BankDepartmentServiceModel> {department};
            return SingleResult.Create(query.AsQueryable());
        }

        // GET: odata/CurrencyRateByTimes(5)/Currency
        [EnableQuery]
        public async Task<SingleResult<Currency>> GetCurrency([FromODataUri] string key)
        {
            var currencyRateByTime = await _currencyRateByTimeService.GetById(key);
            var currency = currencyRateByTime?.Currency;
            var query = new List<Currency> {currency};
            return SingleResult.Create(query.AsQueryable());
        }
    }
}
