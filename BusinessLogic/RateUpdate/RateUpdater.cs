using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessLogic.Helpers.Interfacies;
using BusinessLogic.RateUpdate.Interfacies;
using BusinessLogic.Services.Interfacies;
using DataAccess.DataBase;
using DataAccess.DataBase.ModelsHelpers;
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
        private readonly IBankService _bankService;
        private readonly IParser _parser;
        private readonly IReader _reader;
        private readonly IUnitOfWork _unitOfWork;

        public RateUpdater(ICityRepository cityRepository, ICurrencyRepository currencyRepository,
                            IParser parser, IReader reader, IBankService bankService, IUnitOfWork unitOfWork)
        {
            _bankService = bankService;
            _unitOfWork = unitOfWork;
            _reader = reader;
            _parser = parser;
            _cityRepository = cityRepository;
            _currencyRepository = currencyRepository;
        }

        public async Task Update()
        {
            var dateTime = DateTime.UtcNow;
            var cities = await _cityRepository.GetAll();
            var currencies = await _currencyRepository.GetAll();
            var tasks = (from city in cities
                         from currency in currencies
                         select DepartmentsByAllPages(city, currency, dateTime))
                         .ToList();
            await Task.WhenAll(tasks);
            var results = new List<BankServiceModel>();
            tasks.ForEach(x => results.IncludeSequence(x.Result));
            
            await _bankService.IncludeSequenceToDataBaseAsync(results);
            await _unitOfWork.SaveChangesAsync();
        }

        private static string TransformUrl(string city, string currency)
        {
            var urlWithData = new StringBuilder(Url);
            urlWithData = urlWithData.Replace("{currency}", currency.TrimEnd()).Replace("{city}", city.TrimEnd());
            return urlWithData.ToString();
        }

        private async Task<List<BankServiceModel>> DepartmentsByAllPages(CityServiceModel city, CurrencyServiceModel currency, DateTime dateTime)
        {
            var banks = new List<BankServiceModel>();
            string html;
            var pageNumber = 0;
            do
            {
                pageNumber++;
                var urlWithData = TransformUrl(city.Name, currency.Name);
                html = await _reader.HttpClientRead(urlWithData + pageNumber);
                var pagesBanks =_parser.ParsToIncomingBanks(html, city.Id, currency.Id, dateTime);
                banks.IncludeSequence(pagesBanks);
            } while (_parser.HasNextPage(html));
            return banks;
        }

    }
}