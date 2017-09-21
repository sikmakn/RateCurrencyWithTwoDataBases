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
using DataAccess.Repositories.Interfacies;
using DataAccess.UnitOfWork;

namespace BusinessLogic.RateUpdate
{
    public class RateUpdater: IRateUpdater
    {
        private const string Url = "https://finance.tut.by/kurs/{city}/{currency}/vse-banki/?iPageNo=";

        private readonly IDictionaryRepository<City> _cityRepository;
        private readonly IDictionaryRepository<Currency> _currencyRepository;
        private readonly IBankService _bankService;
        private readonly IParser _parser;
        private readonly IReader _reader;
        private readonly IUnitOfWork _unitOfWork;

        public RateUpdater(IDictionaryRepository<City> cityRepository, IDictionaryRepository<Currency> currencyRepository,
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
            var cities = _cityRepository.GetAll();
            var currencies = _currencyRepository.GetAll();
            var tasks = (from city in cities
                         from currency in currencies
                         select DepartmentsByAllPages(city, currency, dateTime))
                         .ToList();
            await Task.WhenAll(tasks);
            var results = new List<Bank>();
            tasks.ForEach(x => results.IncludeSequence(x.Result));
            
            _bankService.IncludeSequenceToDataBase(results);
            await _unitOfWork.SaveChangesAsync();
        }

        private static string TransformUrl(string city, string currency)
        {
            var urlWithData = new StringBuilder(Url);
            urlWithData = urlWithData.Replace("{currency}", currency.TrimEnd()).Replace("{city}", city.TrimEnd());
            return urlWithData.ToString();
        }

        private async Task<List<Bank>> DepartmentsByAllPages(City city, Currency currency, DateTime dateTime)
        {
            var banks = new List<Bank>();
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