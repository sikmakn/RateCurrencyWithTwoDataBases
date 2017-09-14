using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessLogic.Helpers.Interfacies;
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
        private readonly IBankRepository _bankRepository;
        private readonly IParser _parser;
        private readonly IReader _reader;
        private readonly IUnitOfWork _unitOfWork;

        public RateUpdater(DictionaryRepository<City> cityRepository, DictionaryRepository<Currency> currencyRepository,
                            IParser parser, IReader reader, IBankRepository bankRepository, IUnitOfWork unitOfWork)
        {
            _bankRepository = bankRepository;
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

            var banks = new List<Bank>();
            foreach (var city in cities)
            {
                foreach (var currency in currencies)
                {
                    await DepartmentsByAllPagesToIncomingBanks(banks, city, currency, dateTime);
                }
            }
            AddBanksIsNotExistOrAddDepartments(banks);
            await _unitOfWork.SaveChangesAsync();
        }

        private void AddBanksIsNotExistOrAddDepartments(IEnumerable<Bank> banks)
        {
            foreach (var bank in banks)
            {
                var oldBank = _bankRepository.FindByName(bank.Name);
                if (oldBank == null)
                {
                    _bankRepository.Add(bank);
                }
                else
                {
                    AddDepartmetIsNotExistOrAddRate(bank.BankDepartment, oldBank.BankDepartment);
                }
            }
        }

        private void AddDepartmetIsNotExistOrAddRate(IEnumerable<BankDepartment> departments, ICollection<BankDepartment> oldDepartments)
        {
            foreach (var department in departments)
            {
                var oldDepartment = oldDepartments.FirstOrDefault(x =>
                    x.Name.Contains(department.Name) && x.Address.Contains(department.Address));
                if (oldDepartment == null)
                {
                    oldDepartments.Add(department);
                }
                else
                {
                    foreach (var rateByTime in department.CurrencyRateByTime)
                    {
                        rateByTime.BankDepartment = oldDepartment;
                        rateByTime.BankDepartmentId = oldDepartment.Id;
                        oldDepartment.CurrencyRateByTime.Add(rateByTime);
                    }
                }
            }
        }

        private string TransformUrl(string city, string currency)
        {
            var urlWithData = new StringBuilder(url);
            urlWithData = urlWithData.Replace("{currency}", currency.TrimEnd()).Replace("{city}", city.TrimEnd());
            return urlWithData.ToString();
        }

        private async Task DepartmentsByAllPagesToIncomingBanks(List<Bank> incomingBanks, City city, Currency currency, DateTime dateTime)
        {
            string html;
            var pageNumber = 0;

            do
            {
                pageNumber++;
                var urlWithData = TransformUrl(city.Name, currency.Name);
                html = await _reader.HttpClientRead(urlWithData + pageNumber);
                await _parser.ParsToIncomingBanks(incomingBanks, html, city.Id, currency.Id, dateTime);

            } while (_parser.HasNextPage(html));

        }
        
    }
}