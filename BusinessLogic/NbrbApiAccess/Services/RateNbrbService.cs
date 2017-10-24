using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Web.Script.Serialization;
using BusinessLogic.Helpers.Interfacies;
using BusinessLogic.NbrbApiAccess.Models;
using BusinessLogic.NbrbApiAccess.Services.Interfacies;

namespace BusinessLogic.NbrbApiAccess.Services
{
    public class RateNbrbService: IRateNbrbService
    {
        private readonly IReader _reader;
        private const string Uri = "http://www.nbrb.by/API/ExRates";
        private static readonly JavaScriptSerializer JsonSerializer = new JavaScriptSerializer();

        public RateNbrbService(IReader reader)
        {
            _reader = reader;
        }

        #region IRateNbrb

        public async Task<IEnumerable<RateShortNbrb>> ReadCurrenciesNbrb(int currencyId, DateTime startDate, DateTime endDate)
        {
            var uriDynamics = GetUriDynamics(currencyId);
            var results = new List<RateShortNbrb>();

            InitTimePeriods(startDate, endDate, out var startPeriod, out var endPeriod);
            while (endPeriod <= endDate)
            {
                var resultByPeriod = await GetResultsByPeriod(uriDynamics, startPeriod, endPeriod);
                results.AddRange(resultByPeriod);
                if (endPeriod == endDate) break;
                RecountDates(ref startPeriod, ref endPeriod, ref endDate);
            };

            return results;
        }

        #endregion

        #region PrivateMethods

        private async Task<List<RateShortNbrb>> GetResultsByPeriod(string uriDynamics, DateTime startPeriod, DateTime endPeriod)
        {
            var uri = GetUriWithDates(uriDynamics, startPeriod, endPeriod);
            return await GetObjectListByUriAsync<RateShortNbrb>(uri);
        }

        private static string GetUriDynamics(int currencyId)
        {
            var uriBuilder = new StringBuilder(Uri);
            uriBuilder = uriBuilder.Append("/Rates/Dynamics/").Append(currencyId);
            return uriBuilder.ToString();
        }

        private static void InitTimePeriods(DateTime startDate, DateTime endDate, out DateTime startPeriod, out DateTime endPeriod)
        {
            startPeriod = startDate;
            var startPeriodWithAddedYear = startPeriod.AddDays(365);
            endPeriod = startPeriodWithAddedYear < endDate ? startPeriodWithAddedYear : endDate;
        }

        private static string GetUriWithDates(string uri, DateTime startPeriod, DateTime endPeriod)
        {
            var uriBuilder = new StringBuilder(uri);
            uriBuilder.Append($"?&startDate={startPeriod.ToString("u").Split(' ')[0]}&endDate={endPeriod.ToString("u").Split(' ')[0]}");
            return uriBuilder.ToString();
        }

        private static void RecountDates(ref DateTime startPeriod, ref DateTime endPeriod, ref DateTime endDate)
        {
            var startPeriodWithAddedYear = startPeriod.AddDays(365);
            startPeriod = startPeriodWithAddedYear < endDate ? startPeriodWithAddedYear : endPeriod;
            startPeriodWithAddedYear = startPeriodWithAddedYear.AddDays(365);
            endPeriod = startPeriodWithAddedYear < endDate ? startPeriodWithAddedYear : endDate;

        }

        private async Task<List<T>> GetObjectListByUriAsync<T>(string uri)
        {
                var json = await _reader.HttpClientRead(uri);
                return JsonSerializer.Deserialize<List<T>>(json);
        }

        #endregion
    }
}
