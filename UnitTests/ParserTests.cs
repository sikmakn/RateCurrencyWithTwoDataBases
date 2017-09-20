using System;
using System.Collections.Generic;
using System.IO;
using BusinessLogic.RateUpdate;
using DataAccess.DataBase;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace UnitTests
{
    [TestClass]
    public class ParserTests
    {
        #region HasNextPageMethodTests

        [TestMethod]
        [ExpectedException(typeof(NullReferenceException), "Nullable html argument")]
        public void TestForNullHasNextPage()
        {
            var parser = new Parser();

            parser.HasNextPage(null);
        }

        [TestMethod]
        public void TestForEmptyHasNextPage()
        {
            var parser = new Parser();

            var result = parser.HasNextPage("");

            Assert.IsFalse(result);
        }

        [TestMethod]
        public void TestIncorrectHtmlHasNextPage()
        {
            var parser = new Parser();

            var result = parser.HasNextPage("gvvjhvkjhjvjhvjknjnkj");

            Assert.IsFalse(result);
        }

        [TestMethod]
        public void TestCorrectFalseHasNextPage()
        {
            var parser = new Parser();

            var result = parser.HasNextPage("<body><a></a><span></span></body>");

            Assert.IsFalse(result);
        }

        [TestMethod]
        public void TestCorrectTrueHasNextPage()
        {
            var parser = new Parser();

            var result = parser.HasNextPage("<body><a title='Следующая страница'></a></body>");

            Assert.IsTrue(result);
        }

        #endregion

        #region ParsToIncomingBanks
        [TestMethod]
        public void TestCorrectParsToIncomingBanks()
        {
            //var text = new Reader().HttpClientRead("https://finance.tut.by/kurs/minsk/dollar/vse-banki/?iPageNo=1").Result;
            const string path = @"../../Files/Correct1.txt";
            //File.WriteAllText(path, text);
           
            var parser = new Parser();
            var html = File.ReadAllText(path);
            const int cityId = 2; //minsk
            const int currencyId = 2; //dollar

            var result = parser.ParsToIncomingBanks(html, cityId, currencyId, DateTime.UtcNow);

            Assert.IsNotNull(result);
            Assert.AreNotEqual(0, result.Count);
            var departmentCount = 0;
            result.ForEach(x => departmentCount += x.BankDepartment.Count);
            Assert.AreEqual(20, departmentCount);

            foreach (var bank in result)
            {
                foreach (var department in bank.BankDepartment)
                {
                    Assert.AreEqual(cityId, department.CityId);
                    foreach (var rateByTime in department.CurrencyRateByTime)
                    {
                        Assert.AreEqual(currencyId, rateByTime.CurrencyId);
                    }
                }
            }

        }

        [TestMethod]
        [ExpectedException(typeof(NullReferenceException), "Nullable html argument")]
        public void TestNullableHtmlParsToIncomingBanks()
        {
            var parser = new Parser();
            const int cityId = 2; //minsk
            const int currencyId = 2; //dollar

            var result = new List<Bank>();
            parser.ParsToIncomingBanks(null, cityId, currencyId, DateTime.UtcNow);
        }

        [TestMethod]
        public void TestEmptyHtmlParsToIncomingBanks()
        {
            var parser = new Parser();
            const int cityId = 2; //minsk
            const int currencyId = 2; //dollar

            var result = new List<Bank>();
            parser.ParsToIncomingBanks("", cityId, currencyId, DateTime.UtcNow);

            Assert.AreEqual(0, result.Count);
        }

        [TestMethod]
        public void TestEmptyTableHtmlHtmlParsToIncomingBanks()
        {
            var parser = new Parser();
            const int cityId = 2; //minsk
            const int currencyId = 2; //dollar

            var result = new List<Bank>();
            parser.ParsToIncomingBanks("<body><table class='tbl m-tbl'><tr><td></td></tr></table><span></span></body>", cityId, currencyId, DateTime.UtcNow);

            Assert.AreEqual(0, result.Count);
        }

        [TestMethod]
        public void TestIncorrectTableHtmlHtmlParsToIncomingBanks()
        {
            var parser = new Parser();
            const int cityId = 2; //minsk
            const int currencyId = 2; //dollar

            var result = new List<Bank>();
            parser.ParsToIncomingBanks("<body><table><tr><td></td></tr></table><span></span></body>", cityId, currencyId, DateTime.UtcNow);

            Assert.AreEqual(0, result.Count);
        }

        [TestMethod]
        public void TestIncorrectHtmlHtmlParsToIncomingBanks()
        {
            var parser = new Parser();
            const int cityId = 2; //minsk
            const int currencyId = 2; //dollar

            var result = new List<Bank>();
            parser.ParsToIncomingBanks("gbsfbaraebfbelafbkbfjkbfjebfebwjhfbaklefjh", cityId, currencyId, DateTime.UtcNow);

            Assert.AreEqual(0, result.Count);
        }
        #endregion
    }
}
