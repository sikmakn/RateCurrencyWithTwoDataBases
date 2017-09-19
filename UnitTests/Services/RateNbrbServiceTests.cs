using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web.Script.Serialization;
using BusinessLogic.Helpers;
using BusinessLogic.Helpers.Interfacies;
using BusinessLogic.NbrbApiAccess.Models;
using BusinessLogic.NbrbApiAccess.Services;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace UnitTests.Services
{
    [TestClass]
    public class RateNbrbServiceTests
    {

        [TestMethod]
        public void TestAtTheRightTimePeriods()
        {
            var rates = Init();
            var readerMock = new Mock<IReader>();
            readerMock.Setup(s => s.HttpClientRead(It.IsAny<string>())).ReturnsAsync((string uri) => HttpClientMock(uri, rates));

            var rateService = new RateNbrbService(readerMock.Object);

            var result = rateService.ReadCurrenciesNbrb(1, new DateTime(2014, 07, 05), new DateTime(2016, 07, 05)).Result;
            Assert.IsNotNull(result);
            Assert.AreEqual(5, result.Count());
            for (int i = 0, j = 1; i < result.Count(); i++, j++)
            {
                Assert.AreEqual(rates[j].Cur_ID, result.ElementAt(i).Cur_ID);
            }
        }

        [TestMethod]
        public void TestWithEmptyPeriods()
        {
            var rates = new List<RateShortNbrb>();
            var readerMock = new Mock<IReader>();
            readerMock.Setup(s => s.HttpClientRead(It.IsAny<string>())).ReturnsAsync((string uri) => HttpClientMock(uri, rates));

            var rateService = new RateNbrbService(readerMock.Object);

            var result = rateService.ReadCurrenciesNbrb(1, new DateTime(2014, 07, 05), new DateTime(2016, 07, 05)).Result;
            Assert.IsNotNull(result);
            Assert.AreEqual(0, result.Count());
        }

        [TestMethod]
        public void TestWithEmptyStartPeriod()
        {
            var rates = Init();
            var readerMock = new Mock<IReader>();
            readerMock.Setup(s => s.HttpClientRead(It.IsAny<string>())).ReturnsAsync((string uri) => HttpClientMock(uri, rates));

            var rateService = new RateNbrbService(readerMock.Object);

            var result = rateService.ReadCurrenciesNbrb(1, new DateTime(2012, 07, 05), new DateTime(2016, 07, 05)).Result;
            Assert.IsNotNull(result);
            Assert.AreEqual(6, result.Count());
            for (int i = 0, j = 0; i < result.Count(); i++, j++)
            {
                Assert.AreEqual(rates[j].Cur_ID, result.ElementAt(i).Cur_ID);
            }
        }

        [TestMethod]
        public void TestWithEmptyEndPeriod()
        {
            var rates = Init();
            var readerMock = new Mock<IReader>();
            readerMock.Setup(s => s.HttpClientRead(It.IsAny<string>())).ReturnsAsync((string uri) => HttpClientMock(uri, rates));

            var rateService = new RateNbrbService(readerMock.Object);

            var result = rateService.ReadCurrenciesNbrb(1, new DateTime(2014, 07, 05), new DateTime(2019, 07, 05)).Result;
            Assert.IsNotNull(result);
            Assert.AreEqual(6, result.Count());
            for (int i = 0, j = 1; i < result.Count(); i++, j++)
            {
                Assert.AreEqual(rates[j].Cur_ID, result.ElementAt(i).Cur_ID);
            }
        }

        [TestMethod]
        public void TestWithEmptyStartAndEndPeriod()
        {
            var rates = Init();
            var readerMock = new Mock<IReader>();
            readerMock.Setup(s => s.HttpClientRead(It.IsAny<string>())).ReturnsAsync((string uri) => HttpClientMock(uri, rates));

            var rateService = new RateNbrbService(readerMock.Object);

            var result = rateService.ReadCurrenciesNbrb(1, new DateTime(2012, 07, 05), new DateTime(2019, 07, 05)).Result;
            Assert.IsNotNull(result);
            Assert.AreEqual(7, result.Count());
            for (int i = 0, j = 0; i < result.Count(); i++, j++)
            {
                Assert.AreEqual(rates[j].Cur_ID, result.ElementAt(i).Cur_ID);
            }
        }

        [TestMethod]
        public void TestWithInccorrectCurrencyIdPeriod()
        {
            var rateService = new RateNbrbService(new Reader());

            var result = rateService.ReadCurrenciesNbrb(-17000000, new DateTime(2012, 07, 05), new DateTime(2019, 07, 05)).Result;
            Assert.IsNotNull(result);
            Assert.AreEqual(0, result.Count());
        }

        private static string HttpClientMock(string uri, IEnumerable<RateShortNbrb> rates)
        {
            if(!uri.Contains("http://www.nbrb.by/API/ExRates/Rates/Dynamics/")) throw new InvalidDataException("Invalid uri address");
            var splitStrings = uri.Split('=');
            var startDateString = splitStrings[1].Split('&');
            var startDate = Convert.ToDateTime(startDateString[0]);
            var endDate = Convert.ToDateTime(splitStrings[2]);
            var startDateWithAddedYear = startDate.AddDays(365);
            var result = rates.Where(x => x.Date >= startDate && x.Date <= endDate && startDateWithAddedYear > x.Date);

            var jsonSerializer = new JavaScriptSerializer();
            return jsonSerializer.Serialize(result);
        }

        private static List<RateShortNbrb> Init()
        {
            var rateShort0 = new RateShortNbrb
            {
                Cur_ID = 0,
                Cur_OfficialRate = new decimal(2.00),
                Date = new DateTime(2014, 07, 04)
            };
            
            var rateShort1 = new RateShortNbrb
            {
                Cur_ID = 1,
                Cur_OfficialRate = new decimal(2.02),
                Date = new DateTime(2014, 07, 05)
            };

            var rateShort2 = new RateShortNbrb
            {
                Cur_ID = 2,
                Cur_OfficialRate = new decimal(2.03),
                Date = new DateTime(2014, 08, 05)
            };

            var rateShort3 = new RateShortNbrb
            {
                Cur_ID = 3,
                Cur_OfficialRate = new decimal(2.04),
                Date = new DateTime(2015, 07, 05)
            };

            var rateShort4 = new RateShortNbrb
            {
                Cur_ID = 4,
                Cur_OfficialRate = new decimal(2.05),
                Date = new DateTime(2015, 08, 05)
            };

            var rateShort5 = new RateShortNbrb
            {
                Cur_ID = 5,
                Cur_OfficialRate = new decimal(2.05),
                Date = new DateTime(2016, 07, 05)
            };

            var rateShort6 = new RateShortNbrb
            {
                Cur_ID = 6,
                Cur_OfficialRate = new decimal(2.07),
                Date = new DateTime(2017, 07, 06)
            };
            var ratesList = new List<RateShortNbrb>
            {
                rateShort0,
                rateShort1,
                rateShort2,
                rateShort3,
                rateShort4,
                rateShort5,
                rateShort6
            };
            return ratesList;
        }

    }
}
