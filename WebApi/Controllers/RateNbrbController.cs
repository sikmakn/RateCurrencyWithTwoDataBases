using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Http;
using BusinessLogic.BnrbApiAccess.Services.Interfacies;
using DataAccess.Models;

namespace WebApi.Controllers
{
    public class RateNbrbController : ApiController
    {
        private readonly IRateNbrbService _rateNbrbService;

        public RateNbrbController(IRateNbrbService rateNbrbService)
        {
            _rateNbrbService = rateNbrbService;
        }

        public async Task<IEnumerable<RateShortNbrb>> Get(int currencyId, DateTime startDate, DateTime endDate)
        {
            return await _rateNbrbService.ReadAllCurrencyBnrbs(currencyId, startDate, endDate);
        }
    }
}
