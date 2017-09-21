using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using DataAccess.DataBase;
using DataAccess.DataBase.ModelsHelpers;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace UnitTests.EFEntitiesHelpers
{
    [ExcludeFromCodeCoverage]
    [TestClass]
    public class BankDepartmentHelperTests
    {
        #region FindDepartmentInEnumerable
        [TestMethod]
        [ExpectedException(typeof(NullReferenceException), "Nullable input argument")]
        public void TestNullableInput()
        {
            var departments = new List<BankDepartment>
            {
                new BankDepartment
                {
                    Address = "Address1",
                    Name = "Name1"
                },
                new BankDepartment
                {
                    Address = "Address2",
                    Name = "Name2"
                }
            };

            var result = departments.FindDepartmentInSequence(null);
        }

        [TestMethod]
        public void TestNullableInputName()
        {
            var departments = new List<BankDepartment>
            {
                new BankDepartment
                {
                    Address = "Address1",
                    Name = "Name1"
                },
                new BankDepartment
                {
                    Address = "Address2",
                    Name = "Name2"
                }
            };

            var result = departments.FindDepartmentInSequence(new BankDepartment{Address = "Address"});
            Assert.IsNull(result);
        }

        [TestMethod]
        public void TestNullableInputNameAndCorrectAddress()
        {
            var departments = new List<BankDepartment>
            {
                new BankDepartment
                {
                    Address = "Address1",
                    Name = "Name1"
                },
                new BankDepartment
                {
                    Address = "Address2",
                    Name = "Name2"
                }
            };

            var result = departments.FindDepartmentInSequence(new BankDepartment { Address = "Address1" });
            Assert.IsNull(result);
        }

        [TestMethod]
        public void TestEmptyInput()
        {
            var departments = new List<BankDepartment>
            {
                new BankDepartment
                {
                    Address = "Address1",
                    Name = "Name1"
                },
                new BankDepartment
                {
                    Address = "Address2",
                    Name = "Name2"
                }
            };

            var result = departments.FindDepartmentInSequence(new BankDepartment());
            Assert.IsNull(result);
        }

        [TestMethod]
        public void TestCorrectInput()
        {
            var bank1 = new BankDepartment
            {
                Address = "Address1",
                Name = "Name1"
            };
            var departments = new List<BankDepartment>
            {
                bank1,
                new BankDepartment
                {
                    Address = "Address2",
                    Name = "Name2"
                }
            };

            var result = departments.FindDepartmentInSequence(new BankDepartment
            {
                Name = "Name1",
                Address = "Address1"
            });
            Assert.IsNotNull(result);
            Assert.AreEqual(result, bank1);
        }

        [TestMethod]
        public void TestEmptyNameSourceInput()
        {
            var bank1 = new BankDepartment
            {
                Address = "",
                Name = "Name1"
            };
            var departments = new List<BankDepartment>
            {
                bank1,
                new BankDepartment
                {
                    Address = "Address2",
                    Name = "Name2"
                }
            };

            var result = departments.FindDepartmentInSequence(new BankDepartment
            {
                Name = "Name1",
                Address = "Address1"
            });
            Assert.IsNull(result);
        }

        [TestMethod]
        public void TestNullableNameSourceInput()
        {
            var bank1 = new BankDepartment
            {
                Address = null,
                Name = "Name1"
            };
            var departments = new List<BankDepartment>
            {
                bank1,
                new BankDepartment
                {
                    Address = "Address2",
                    Name = "Name2"
                }
            };

            var result = departments.FindDepartmentInSequence(new BankDepartment
            {
                Name = "Name1",
                Address = "Address1"
            });
            Assert.IsNull(result);
        }

        #endregion

        #region Equals

        [TestMethod]
        [ExpectedException(typeof(NullReferenceException), "Nullable input argument")]
        public void TestNullableInputEquals()
        {
            var department = new BankDepartment
            {
                Address = "Address1",
                Name = "Name1"
            };

            var result = department.EqualsByNameAndAddress(null);
            
            Assert.IsFalse(result);
        }

        [TestMethod]
        public void TestNullableInputNameEquals()
        {
            var department = new BankDepartment
            {
                Address = "Address1",
                Name = "Name1"
            };

            var result = department.EqualsByNameAndAddress(new BankDepartment { Address = "Address" });
            Assert.IsFalse(result);
        }

        [TestMethod]
        public void TestNullableInputNameAndCorrectAddressEquals()
        {
            var department = new BankDepartment
            {
                Address = "Address1",
                Name = "Name1"
            };

            var result = department.Equals(new BankDepartment { Address = "Address1" });
            Assert.IsFalse(result);
        }

        [TestMethod]
        public void TestEmptyInputEquals()
        {
            var department = new BankDepartment
            { 
                    Address = "Address1",
                    Name = "Name1"
            };

            var result = department.Equals(new BankDepartment());
            Assert.IsFalse(result);
        }

        [TestMethod]
        public void TestCorrectInputEquals()
        {
            var bank1 = new BankDepartment
            {
                Address = "Address1",
                Name = "Name1"
            };

            var result = bank1.EqualsByNameAndAddress(new BankDepartment
            {
                Name = "Name1",
                Address = "Address1"
            });
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void TestEmptyNameSourceInputEquals()
        {
            var bank1 = new BankDepartment
            {
                Address = "",
                Name = "Name1"
            };

            var result = bank1.EqualsByNameAndAddress(new BankDepartment
            {
                Name = "Name1",
                Address = "Address1"
            });
            Assert.IsFalse(result);
        }

        [TestMethod]
        public void TestNullableNameSourceInputEquals()
        {
            var bank1 = new BankDepartment
            {
                Address = null,
                Name = "Name1"
            };

            var result = bank1.EqualsByNameAndAddress(new BankDepartment
            {
                Name = "Name1",
                Address = "Address1"
            });
            Assert.IsFalse(result);
        }

        #endregion
        
        #region IncludeSequenceTests

        [TestMethod]
        public void TestEmptySourceSequence()
        {
            var bankDepartment1 = new BankDepartment()
            {
                Name = "BankDepartment1"
            };
            var bankDepartment2 = new BankDepartment()
            {
                Name = "BankDepartment2"
            };

            var sourceList = new List<BankDepartment>();
            var bankDepartments = new List<BankDepartment>
            {
                bankDepartment1,
                bankDepartment2
            };

            sourceList.IncludeSequence(bankDepartments);

            Assert.IsNotNull(sourceList);
            Assert.AreEqual(2, sourceList.Count);
        }

        [TestMethod]
        public void TestDifferenceSequence()
        {
            var bank1 = new BankDepartment
            {
                Name = "Bank1",
                Address = "Address1"
            };
            var bank2 = new BankDepartment
            {
                Name = "Bank2",
                Address = "Address2"
            };
            var bank3 = new BankDepartment
            {
                Name = "bank3",
                Address = "Address3"
            };
            var sourceList = new List<BankDepartment>
            {
                bank3
            };
            var banks = new List<BankDepartment>
            {
                bank1,
                bank2
            };

            sourceList.IncludeSequence(banks);

            Assert.IsNotNull(sourceList);
            Assert.AreEqual(3, sourceList.Count);
            Assert.IsTrue(sourceList[0].EqualsByNameAndAddress(bank3));
            Assert.IsTrue(sourceList[1].EqualsByNameAndAddress(bank1));
            Assert.IsTrue(sourceList[2].EqualsByNameAndAddress(bank2));
        }

        [TestMethod]
        public void TestSameSequence()
        {
            var bank1 = new BankDepartment
            {
                Name = "Bank1",
                Address = "Address1"
            };
            var bank2 = new BankDepartment
            {
                Name = "Bank2",
                Address = "Address2"
            };
            var bank3 = new BankDepartment
            {
                Name = "Bank1",
                Address = "Address1"
            };
            var sourceList = new List<BankDepartment>
            {
                bank3
            };
            var banks = new List<BankDepartment>
            {
                bank1,
                bank2
            };

            sourceList.IncludeSequence(banks);

            Assert.IsNotNull(sourceList);
            Assert.AreEqual(2, sourceList.Count);
        }

        [TestMethod]
        public void TestSameSequenceWithDifferenceCurrencyRates()
        {
            var bank1 = new BankDepartment
            {
                Name = "Bank1",
                Address = "Address1",
                CurrencyRateByTime = new List<CurrencyRateByTime>
                {
                    new CurrencyRateByTime
                    {
                        Purchase = 1,
                        Sale = 1
                    }
                }
            };
            var bank2 = new BankDepartment
            {
                Name = "Bank2",
                Address = "Address2",
                CurrencyRateByTime = new List<CurrencyRateByTime>
                {
                    new CurrencyRateByTime
                    {
                        Purchase = 2,
                        Sale = 2
                    }
                }
            };
            var bank3 = new BankDepartment
            {
                Name = "Bank1",
                Address = "Address1",
                CurrencyRateByTime = new List<CurrencyRateByTime>
                {
                    new CurrencyRateByTime
                    {
                        Purchase = 3,
                        Sale = 3
                    }
                }
            };
            var sourceList = new List<BankDepartment>
            {
                bank3
            };
            var banks = new List<BankDepartment>
            {
                bank1,
                bank2
            };
            sourceList.IncludeSequence(banks);

            Assert.IsNotNull(sourceList);
            Assert.AreEqual(2, sourceList.Count);
            Assert.AreEqual(2, bank3.CurrencyRateByTime.Count);
            Assert.AreEqual(1, bank2.CurrencyRateByTime.Count);
        }

        #endregion
    }
}
