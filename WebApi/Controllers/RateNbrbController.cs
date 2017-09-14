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
        // GET: api/RateNbrb
        public async Task<IEnumerable<RateShortNbrb>> Get(int currencyId, DateTime startDate, DateTime endDate)
        {
            return await _rateNbrbService.ReadAllCurrencyBnrbs(currencyId, startDate, endDate);
        }

        // GET: api/RateNbrb/5
        public string Get(int id)
        {
            return "value";
        }

        // POST: api/RateNbrb
        public void Post([FromBody]string value)
        {
        }

        // PUT: api/RateNbrb/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/RateNbrb/5
        public void Delete(int id)
        {
        }
    }
}
