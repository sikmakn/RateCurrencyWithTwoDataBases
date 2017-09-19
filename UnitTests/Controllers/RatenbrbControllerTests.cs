using System;
using System.Collections.Generic;
using System.Linq;
using BusinessLogic.NbrbApiAccess.Models;
using BusinessLogic.NbrbApiAccess.Services.Interfacies;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using WebApi.Controllers;

namespace UnitTests.Controllers
{
    [TestClass]
    public class RateNbrbControllerTests
    {
        [TestMethod]
        public void TestByNull()
        {
            var rateNbrbServiceMock = new Mock<IRateNbrbService>();
            rateNbrbServiceMock
                .Setup(s => s.ReadCurrenciesNbrb(It.IsAny<int>(), It.IsAny<DateTime>(), It.IsAny<DateTime>()))
                .ReturnsAsync((int a, DateTime b, DateTime c) => new List<RateShortNbrb>());
            var rateController = new RateNbrbController(rateNbrbServiceMock.Object);

            var result = rateController.Get(0, DateTime.MaxValue, DateTime.MaxValue).Result;

            Assert.IsNotNull(result);
            Assert.AreEqual(0, result.Count());
        }
    }
}
