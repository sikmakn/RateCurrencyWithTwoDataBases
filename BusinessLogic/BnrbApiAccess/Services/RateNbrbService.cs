using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Web.Script.Serialization;
using BusinessLogic.BnrbApiAccess.Services.Interfacies;
using BusinessLogic.Helpers.Interfacies;
using DataAccess.Models;

namespace BusinessLogic.BnrbApiAccess.Services
{
    public class RateNbrbService: IRateNbrbService
    {
        private readonly IReader _reader;
        private const string URI = "http://www.nbrb.by/API/ExRates";
        private static readonly JavaScriptSerializer JsonSerializer = new JavaScriptSerializer();

        public RateNbrbService(IReader reader)
        {
            _reader = reader;
        }

        public async Task<IEnumerable<RateShortNbrb>> ReadAllCurrencyBnrbs(int currencyId, DateTime startDate, DateTime endDate)
        {
            var uriBuilder = new StringBuilder(URI);
            uriBuilder = uriBuilder.Append("/Rates/Dynamics/").Append(currencyId);

            var results = new List<RateShortNbrb>();

            var startPeriod = startDate;
            var startPeriodWithAddedYear = startPeriod.AddYears(1);
            var endPeriod = startPeriodWithAddedYear < endDate ? startPeriodWithAddedYear : endDate;

            while (endPeriod <= endDate)
            {
                var uri = new StringBuilder(uriBuilder.ToString());
                uri.Append($"?&startDate={startPeriod.ToString("u").Split(' ')[0]}&endDate={endPeriod.ToString("u").Split(' ')[0]}");

                var resultByPeriod = await GetObjectListByUriAsync<RateShortNbrb>(uri.ToString());
                results.AddRange(resultByPeriod);
                if(endPeriod == endDate) break;

                startPeriod = startPeriodWithAddedYear < endDate ? startPeriodWithAddedYear : endPeriod;
                startPeriodWithAddedYear = startPeriodWithAddedYear.AddYears(1);
                endPeriod = startPeriodWithAddedYear < endDate ? startPeriodWithAddedYear : endDate;
            }

            return results;
        }

        private async Task<List<T>> GetObjectListByUriAsync<T>(string uri)
        {
            try
            {
                var json = await _reader.HttpClientRead(uri);
                return JsonSerializer.Deserialize<List<T>>(json);
            }
            catch (Exception e)
            {
                return null;
            }
        }
    }
}
