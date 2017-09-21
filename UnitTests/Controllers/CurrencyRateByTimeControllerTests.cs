using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using BusinessLogic.Services.Interfacies;
using DataAccess.DataBase;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using WebApi.Controllers;

namespace UnitTests.Controllers
{
    [ExcludeFromCodeCoverage]
    [TestClass]
    public class CurrencyRateByTimeControllerTests
    {
        private readonly List<CurrencyRateByTime> _currencyRates = InitializeList();

        [TestMethod]
        public void GetBankDepartmentTest()
        {
            var currencyRateServiceMock = new Mock<ICurrencyRateByTimeService>();
            currencyRateServiceMock.Setup(s => s.GetById(1)).ReturnsAsync(_currencyRates[0]);

            var currencyRateController = new CurrencyRateByTimeController(currencyRateServiceMock.Object);

            var result = currencyRateController.GetBankDepartment(1).Result.Queryable.Single();

            Assert.AreEqual(result.Id, _currencyRates[0].BankDepartment.Id);
            Assert.AreEqual(result.BankId, _currencyRates[0].BankDepartment.BankId);
            Assert.AreEqual(result.Name, _currencyRates[0].BankDepartment.Name);
        }

        [TestMethod]
        public void GetByIdTest()
        {
            var currencyRateServiceMock = new Mock<ICurrencyRateByTimeService>();
            currencyRateServiceMock.Setup(s => s.GetById(1)).ReturnsAsync(_currencyRates[0]);

            var currencyRateController = new CurrencyRateByTimeController(currencyRateServiceMock.Object);

            var result = currencyRateController.GetCurrencyRateByTime(1).Result.Queryable.Single();
            Assert.AreEqual(result.Id, _currencyRates[0].Id);
        }

        [TestMethod]
        public void GetCurrencyTest()
        {
            var currencyRateServiceMock = new Mock<ICurrencyRateByTimeService>();
            currencyRateServiceMock.Setup(s => s.GetById(1)).ReturnsAsync(_currencyRates[0]);

            var currencyRateController = new CurrencyRateByTimeController(currencyRateServiceMock.Object);

            var result = currencyRateController.GetCurrency(1).Result.Queryable.Single();

            Assert.AreEqual(result.Id, _currencyRates[0].CurrencyId);
            Assert.AreEqual(result.Name, _currencyRates[0].Currency.Name);
        }

        [TestMethod]
        public void GetByIncorrectId()
        {
            var currencyRateServiceMock = new Mock<ICurrencyRateByTimeService>();
            currencyRateServiceMock.Setup(s => s.GetById(It.IsAny<int>())).ReturnsAsync((int s) => null);

            var currencyRateController = new CurrencyRateByTimeController(currencyRateServiceMock.Object);

            var rateByTimeResult = currencyRateController.GetCurrencyRateByTime(1).Result.Queryable.Single();
            Assert.IsNull(rateByTimeResult);

            var currencyResult = currencyRateController.GetCurrency(1).Result.Queryable.Single();
            Assert.IsNull(currencyResult);

            var bankDepartmentResult = currencyRateController.GetBankDepartment(1).Result.Queryable.Single();
            Assert.IsNull(bankDepartmentResult);
        }


        private static List<CurrencyRateByTime> InitializeList()
        {
            var bank1 = new Bank
            {
                Name = "Bank1",
                Id = 1,
                BankDepartment = new List<BankDepartment>()
            };

            var city1 = new City
            {
                Id = 1,
                Name = "City1",
                BankDepartment = new List<BankDepartment>()
            };
            var bankDepartment = new BankDepartment
            {
                Address = "BankDepartment 1 address",
                Id = 1,
                Name = "BankDepartment1",
                Bank = bank1,
                BankId = bank1.Id,
                CurrencyRateByTime = new List<CurrencyRateByTime>(),
                CityId = city1.Id,
                City = city1
            };
            bank1.BankDepartment.Add(bankDepartment);
            city1.BankDepartment.Add(bankDepartment);

            var currency1 = new Currency
            {
                Id = 1,
                Name = "Currency1",
                CurrencyRateByTime = new List<CurrencyRateByTime>()
            };

            var currencyRate1 = new CurrencyRateByTime
            {
                Id = 1,
                BankDepartment = bankDepartment,
                BankDepartmentId = bankDepartment.Id,
                Purchase = 1,
                Sale = 1.1,
                DateTime = DateTime.UtcNow,
                CurrencyId =  currency1.Id,
                Currency = currency1
            };
            currency1.CurrencyRateByTime.Add(currencyRate1);

            var currencyRates = new List<CurrencyRateByTime>
            {
                currencyRate1
            };

            return currencyRates;
        }
    }
}
