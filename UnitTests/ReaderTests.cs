using System;
using BusinessLogic.Helpers;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace UnitTests
{
    [TestClass]
    public class ReaderTests
    {
        [TestMethod]
        [ExpectedException(typeof(AggregateException), "Not valid site uri argument was inappropriately allow.")]
        public void TestNotValidSiteUri()
        {
            var reader = new Reader();
            var result = reader.HttpClientRead("https://dscksdmclskdm.com").Result;
        }

        [TestMethod]
        [ExpectedException(typeof(AggregateException), "Not valid site uri argument was inappropriately allow.")]
        public void TestWithoutHttp()
        {
            var reader = new Reader();
            var result = reader.HttpClientRead("dscksdmclskdm.com").Result;
        }

        [TestMethod]
        [ExpectedException(typeof(AggregateException), "Not valid uri argument was inappropriately allow.")]
        public void TestNotValidUri()
        {
            var reader = new Reader();
            var result = reader.HttpClientRead("dscksdmclskdm").Result;
        }

        [TestMethod]
        [ExpectedException(typeof(AggregateException), "Empty uri argument was inappropriately allow.")]
        public void TestEmptyUri()
        {
            var reader = new Reader();
            var result = reader.HttpClientRead("").Result;
        }

        [TestMethod]
        [ExpectedException(typeof(AggregateException), "Null uri argument was inappropriately allow.")]
        public void TestNullUri()
        {
            var reader = new Reader();
            var result = reader.HttpClientRead(null).Result;
        }

        [TestMethod]
        public void TestValidUri()
        {
            var reader = new Reader();
            var result = reader.HttpClientRead("https://www.tut.by").Result;

            Assert.IsNotNull(result);
            Assert.AreNotEqual(0, result.Length);
        }
    }
}
