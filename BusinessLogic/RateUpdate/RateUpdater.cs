using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessLogic.Helpers.Interfacies;
using BusinessLogic.RateUpdate.Interfacies;
using DataAccess.ModelsForServices;
using DataAccess.Repositories.Interfacies;
using DataAccess.UnitOfWork;

namespace BusinessLogic.RateUpdate
{
    public class RateUpdater: IRateUpdater
    {
        private const string Url = "https://finance.tut.by/kurs/{city}/{currency}/vse-banki/?iPageNo=";

        private readonly ICityRepository _cityRepository;
        private readonly ICurrencyRepository _currencyRepository;
        private readonly IParser _parser;
        private readonly IReader _reader;
        private readonly IUnitOfWork _unitOfWork;
        private readonly ICurrencyRateByTimeRepository _currencyRateByTimeRepository;

        public RateUpdater(ICityRepository cityRepository, ICurrencyRepository currencyRepository,
                            IParser parser, IReader reader, IUnitOfWork unitOfWork, ICurrencyRateByTimeRepository currencyRateByTimeRepository)
        {
            _unitOfWork = unitOfWork;
            _reader = reader;
            _parser = parser;
            _cityRepository = cityRepository;
            _currencyRepository = currencyRepository;
            _currencyRateByTimeRepository = currencyRateByTimeRepository;
        }

        public async Task Update()
        {
            var dateTime = DateTime.UtcNow;
            var cities = await _cityRepository.GetAll();
            var currencies = await _currencyRepository.GetAll();
            var results = new List<CurrencyRateByTimeServiceModel>();
            foreach (var city in cities)
            {
                foreach (var currency in currencies)
                {
                    results.AddRange(await DepartmentsByAllPages(city, currency, dateTime));
                }
            }

            _currencyRateByTimeRepository.BulkAdd(results);
            //var tasks = (from city in cities
            //             from currency in currencies
            //             select DepartmentsByAllPages(city, currency, dateTime))
            //             .ToList();
            //await Task.WhenAll(tasks);
            //tasks.ForEach(t => _currencyRateByTimeRepository.BulkAdd(t.Result));

            await _unitOfWork.SaveChangesAsync();
        }

        private static string TransformUrl(string city, string currency)
        {
            var urlWithData = new StringBuilder(Url);
            urlWithData = urlWithData.Replace("{currency}", currency.TrimEnd()).Replace("{city}", city.TrimEnd());
            return urlWithData.ToString();
        }

        private async Task<List<CurrencyRateByTimeServiceModel>> DepartmentsByAllPages(CityServiceModel city, CurrencyServiceModel currency, DateTime dateTime)
        {
            var currencyRatesList = new List<CurrencyRateByTimeServiceModel>();

            string html;
            var pageNumber = 0;
            do
            {
                pageNumber++;
                var urlWithData = TransformUrl(city.Name, currency.Name);
                html = await _reader.HttpClientRead(urlWithData + pageNumber);
                var pageCurrencyRates = await _parser.ParsToCurrenciesRatesByTimes(html, city, currency, dateTime);
                currencyRatesList.AddRange(pageCurrencyRates);
            } while (_parser.HasNextPage(html));

            return currencyRatesList;
        }

    }
}