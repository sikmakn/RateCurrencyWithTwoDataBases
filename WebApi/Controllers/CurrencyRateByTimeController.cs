using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.OData;
using BusinessLogic.Services.Interfacies;
using DataAccess.DataBase;

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
        public IQueryable<CurrencyRateByTime> GetCurrencyRateByTime()
        {
            return _currencyRateByTimeService.GetAllActual();
        }

        // GET: odata/CurrencyRateByTimes(5)
        [EnableQuery]
        public async Task<SingleResult<CurrencyRateByTime>> GetCurrencyRateByTime([FromODataUri] int key)
        {
            var currencyRateByTime = await _currencyRateByTimeService.GetById(key);
            var query = new List<CurrencyRateByTime> {currencyRateByTime};
            return SingleResult.Create(query.AsQueryable());
        }

        [EnableQuery]
        public async Task<SingleResult<BankDepartment>> GetBankDepartment([FromODataUri] int key)
        {
            var currencyRateByTime = await _currencyRateByTimeService.GetById(key);
            var department = currencyRateByTime.BankDepartment;
            var query = new List<BankDepartment> {department};
            return SingleResult.Create(query.AsQueryable());
        }

        // GET: odata/CurrencyRateByTimes(5)/Currency
        [EnableQuery]
        public async Task<SingleResult<Currency>> GetCurrency([FromODataUri] int key)
        {
            var currencyRateByTime = await _currencyRateByTimeService.GetById(key);
            var currency = currencyRateByTime.Currency;
            var query = new List<Currency> {currency};
            return SingleResult.Create(query.AsQueryable());
        }
    }
}
