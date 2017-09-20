using System.Collections.Generic;
using System.IO;
using System.Linq;
using BusinessLogic.Helpers.Interfacies;
using DataAccess.DataBase;
using DataAccess.Repositories.Interfacies;
using DataAccess.UnitOfWork;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace UnitTests.RateUpdateTests
{
    [TestClass]
    public class RateUpdaterTests
    {
        [TestMethod]
        public void TestCorrectData()
        {
            var bankReposotitoryMock = BankRepositoryMock();
            var cityRepositoryMock = CityRepositoryMock();
            var currencyRepositoryMock = CurrencyRepositoryMock();
            var unitOfWorkMock = UnitOfWorkMock();
            var readerMock = ReaderMock();


        }

  
        private static Mock<IReader> ReaderMock()
        {
            const string path = @"../../Files/Correct1.txt";
            var html = File.ReadAllText(path);
            var readerMock = new Mock<IReader>();
            readerMock.Setup(x => x.HttpClientRead(It.IsAny<string>())).ReturnsAsync(html);
            return readerMock;
        }
        private static Mock<IUnitOfWork> UnitOfWorkMock()
        {
            var unitOfWorkMock = new Mock<IUnitOfWork>();
            unitOfWorkMock.Setup(x => x.SaveChangesAsync()).ReturnsAsync(1);
            return unitOfWorkMock;
        }
        private static Mock<IDictionaryRepository<Currency>> CurrencyRepositoryMock()
        {
            var currencyRepository = new Mock<IDictionaryRepository<Currency>>();
            var currencies = new List<Currency>
            {
                new Currency
                {
                    Id = 1,
                    Name = "dollar"
                },
            };
            currencyRepository.Setup(x => x.GetAll()).Returns(currencies);
            return currencyRepository;
        }

        private static Mock<IDictionaryRepository<City>> CityRepositoryMock()
        {
            var cityRepository =  new Mock<IDictionaryRepository<City>>();
            var cities = new List<City>
            {
                new City
                {
                    Id = 1,
                    Name = "minsk"
                },
            };
            cityRepository.Setup(x => x.GetAll()).Returns(cities);
            return cityRepository;
        }
        private static Mock<IBankRepository> BankRepositoryMock()
        {
            var bankReposotitory = new Mock<IBankRepository>();
            var banks = new Dictionary<string, Bank>();
            var bankId = 0;
            var departmentId = 0;
            bankReposotitory.Setup(x => x.Add(It.IsAny<Bank>())).Returns((Bank b) =>
            {
                b.Id = bankId;
                bankId++;
                foreach (var department in b.BankDepartment)
                {
                    department.BankId = b.Id;
                    department.Bank = b;
                    foreach (var rateByTime in department.CurrencyRateByTime)
                    {
                        rateByTime.BankDepartment = department;
                        rateByTime.BankDepartmentId = departmentId;
                        departmentId++;
                    }
                }
                banks.Add(b.Name, b);
                return b;
            });

            bankReposotitory.Setup(x => x.FindByName(It.IsAny<string>())).Returns((string key) => banks.FirstOrDefault(x => x.Key == key).Value);
            return bankReposotitory;
        }
    }
}
