using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using BusinessLogic.RateUpdate.Interfacies;
using DataAccess.DataBase;
using DataAccess.Repositories;
using DataAccess.Repositories.Interfacies;
using DataAccess.UnitOfWork;

namespace BusinessLogic.RateUpdate
{
    public class RateUpdater: IRateUpdater
    {
        private string url = "https://finance.tut.by/kurs/{city}/{currency}/vse-banki/?iPageNo=";

        private readonly DictionaryRepository<City> _cityRepository;
        private readonly DictionaryRepository<Currency> _currencyRepository;
        private readonly IBankDepartmentRepository _bankDepartmentRepository;
        private readonly IParser _parser;
        private readonly IReader _reader;
        private readonly IUnitOfWork _unitOfWork;

        public RateUpdater(DictionaryRepository<City> cityRepository, DictionaryRepository<Currency> currencyRepository,
                            IParser parser, IBankDepartmentRepository bankDepartmentRepository, IReader reader, 
                            IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            _reader = reader;
            _bankDepartmentRepository = bankDepartmentRepository;
            _parser = parser;
            _cityRepository = cityRepository;
            _currencyRepository = currencyRepository;
        }

        public async Task Update()
        {
            var dateTime = DateTime.UtcNow;
            var cities = _cityRepository.GetAll();
            var currencies = _currencyRepository.GetAll();
            foreach (var city in cities)
            {
                foreach (var currency in currencies)
                {
                    var departments = await DepartmentsByAllPages(city, currency, dateTime);
                    await _bankDepartmentRepository.AddOrUpdatesRange(departments);
                }
            }
            await _unitOfWork.SaveChangesAsync();
        }

        private string TransformUrl(string city, string currency)
        {
            var urlWithData = new StringBuilder(url);
            urlWithData = urlWithData.Replace("{currency}", currency.TrimEnd()).Replace("{city}", city.TrimEnd());
            return urlWithData.ToString();
        }

        private async Task<List<BankDepartment>> DepartmentsByAllPages(City city, Currency currency, DateTime dateTime)
        {
            List<BankDepartment> departments = new List<BankDepartment>();

            string html;
            var pageNumber = 0;
            do
            {
                pageNumber++;

                var urlWithData = TransformUrl(city.Name, currency.Name);
                html = await _reader.HttpClientRead(urlWithData + pageNumber);
                
                var departmentsFromPage = await _parser.Pars(html, city.Id, currency.Id, dateTime);
                departments.AddRange(departmentsFromPage);

            } while (_parser.HasNextPage(html));

            return departments;
        }
        
    }
}