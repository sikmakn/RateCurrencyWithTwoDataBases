
using System;
using System.Collections.Generic;
using DataAccess.DataBase;
using DataAccess.DataBase.ModelsHelpers;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace UnitTests.EFEntitiesHelpers
{
    [TestClass]
    public class BankHelperTests
    {

        #region EqualsByNameMethodTests

        [TestMethod]
        public void TestEqualname()
        {
            var bank1 = new Bank
            {
                Name = "Bank1"
            };
            var bank2 = new Bank
            {
                Name = "Bank1"
            };
            var result = bank1.EqualsByName(bank2);
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void TestNotEqualNames()
        {
            var bank1 = new Bank
            {
                Name = "Bank1"
            };
            var bank2 = new Bank
            {
                Name = "Another bank"
            };
            var result = bank1.EqualsByName(bank2);
            Assert.IsFalse(result);
        }

        [TestMethod]
        public void TestOneEmptyName()
        {
            var bank1 = new Bank
            {
                Name = ""
            };
            var bank2 = new Bank
            {
                Name = "Another bank"
            };
            var result = bank1.EqualsByName(bank2);
            Assert.IsFalse(result);
        }

        [TestMethod]
        public void TestTwoEmptyName()
        {
            var bank1 = new Bank
            {
                Name = ""
            };
            var bank2 = new Bank
            {
                Name = ""
            };
            var result = bank1.EqualsByName(bank2);
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void TestOneNullableName()
        {
            var bank1 = new Bank
            {
                Name = null
            };
            var bank2 = new Bank
            {
                Name = "Another bank"
            };
            var result = bank1.EqualsByName(bank2);
            Assert.IsFalse(result);
        }

        [TestMethod]
        public void TestTwoNullableName()
        {
            var bank1 = new Bank
            {
                Name = null
            };
            var bank2 = new Bank
            {
                Name = null
            };
            var result = bank1.EqualsByName(bank2);
            Assert.IsFalse(result);
        }

        [TestMethod]
        [ExpectedException(typeof(NullReferenceException), "Nullable argument")]
        public void TestSecondNullableBank()
        {
            var bank1 = new Bank
            {
                Name = "Bank1"
            };
            var result = bank1.EqualsByName(null);
            Assert.IsFalse(result);
        }

        [TestMethod]
        [ExpectedException(typeof(NullReferenceException), "Nullable argument")]
        public void TestFirstNullableBank()
        {
            Bank bank1 = null;
            var bank2 = new Bank
            {
                Name = "Another bank"
            };
            var result = bank1.EqualsByName(bank2);
            Assert.IsFalse(result);
        }

        [TestMethod]
        [ExpectedException(typeof(NullReferenceException), "Nullable argument")]
        public void TestNullableBanks()
        {
            Bank bank1 = null;
            var result = bank1.EqualsByName(null);
            Assert.IsFalse(result);
        }
        #endregion
    }
}
