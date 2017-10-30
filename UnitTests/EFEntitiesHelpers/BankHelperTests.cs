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
    public class BankHelperTests
    {

        //#region EqualsByNameMethodTests

        //[TestMethod]
        //public void TestEqualname()
        //{
        //    //var bank1 = new Bank
        //    //{
        //    //    Name = "Bank1"
        //    //};
        //    //var bank2 = new Bank
        //    //{
        //    //    Name = "Bank1"
        //    //};
        //    //var result = bank1.EqualsByName(bank2);
        //    //Assert.IsTrue(result);
        //}

        //[TestMethod]
        //public void TestNotEqualNames()
        //{
        //    var bank1 = new Bank
        //    {
        //        Name = "Bank1"
        //    };
        //    var bank2 = new Bank
        //    {
        //        Name = "Another bank"
        //    };
        //    var result = bank1.EqualsByName(bank2);
        //    Assert.IsFalse(result);
        //}

        //[TestMethod]
        //public void TestOneEmptyName()
        //{
        //    var bank1 = new Bank
        //    {
        //        Name = ""
        //    };
        //    var bank2 = new Bank
        //    {
        //        Name = "Another bank"
        //    };
        //    var result = bank1.EqualsByName(bank2);
        //    Assert.IsFalse(result);
        //}

        //[TestMethod]
        //public void TestTwoEmptyName()
        //{
        //    var bank1 = new Bank
        //    {
        //        Name = ""
        //    };
        //    var bank2 = new Bank
        //    {
        //        Name = ""
        //    };
        //    var result = bank1.EqualsByName(bank2);
        //    Assert.IsTrue(result);
        //}

        //[TestMethod]
        //public void TestOneNullableName()
        //{
        //    var bank1 = new Bank
        //    {
        //        Name = null
        //    };
        //    var bank2 = new Bank
        //    {
        //        Name = "Another bank"
        //    };
        //    var result = bank1.EqualsByName(bank2);
        //    Assert.IsFalse(result);
        //}

        //[TestMethod]
        //public void TestTwoNullableName()
        //{
        //    var bank1 = new Bank
        //    {
        //        Name = null
        //    };
        //    var bank2 = new Bank
        //    {
        //        Name = null
        //    };
        //    var result = bank1.EqualsByName(bank2);
        //    Assert.IsFalse(result);
        //}

        //[TestMethod]
        //[ExpectedException(typeof(NullReferenceException), "Nullable argument")]
        //public void TestSecondNullableBank()
        //{
        //    var bank1 = new Bank
        //    {
        //        Name = "Bank1"
        //    };
        //    bank1.EqualsByName(null);
        //}

        //[TestMethod]
        //[ExpectedException(typeof(NullReferenceException), "Nullable argument")]
        //public void TestFirstNullableBank()
        //{
        //    Bank bank1 = null;
        //    var bank2 = new Bank
        //    {
        //        Name = "Another bank"
        //    };
        //    bank1.EqualsByName(bank2);
        //}

        //[TestMethod]
        //[ExpectedException(typeof(NullReferenceException), "Nullable argument")]
        //public void TestNullableBanks()
        //{
        //    Bank bank1 = null;
        //    bank1.EqualsByName(null);
        //}
        //#endregion

        //#region IncludeSequenceTests

        //[TestMethod]
        //public void TestEmptySourceSequence()
        //{
        //    var bank1 = new Bank
        //    {
        //        Name = "Bank1"
        //    };
        //    var bank2 = new Bank
        //    {
        //        Name = "Bank2"
        //    };

        //    var sourceList = new List<Bank>();
        //    var banks = new List<Bank>
        //    {
        //        bank1,
        //        bank2
        //    };

        //    sourceList.IncludeSequence(banks);

        //    Assert.IsNotNull(sourceList);
        //    Assert.AreEqual(2, sourceList.Count);
        //}

        //[TestMethod]
        //public void TestDifferenceSequence()
        //{
        //    var bank1 = new Bank
        //    {
        //        Name = "Bank1"
        //    };
        //    var bank2 = new Bank
        //    {
        //        Name = "Bank2"
        //    };
        //    var bank3 = new Bank
        //    {
        //        Name = "bank3"
        //    };
        //    var sourceList = new List<Bank>
        //    {
        //        bank3
        //    };
        //    var banks = new List<Bank>
        //    {
        //        bank1,
        //        bank2
        //    };

        //    sourceList.IncludeSequence(banks);

        //    Assert.IsNotNull(sourceList);
        //    Assert.AreEqual(3, sourceList.Count);
        //    Assert.IsTrue(sourceList[0].EqualsByName(bank3));
        //    Assert.IsTrue(sourceList[1].EqualsByName(bank1));
        //    Assert.IsTrue(sourceList[2].EqualsByName(bank2));
        //}

        //[TestMethod]
        //public void TestSameSequence()
        //{
        //    var bank1 = new Bank
        //    {
        //        Name = "Bank1"
        //    };
        //    var bank2 = new Bank
        //    {
        //        Name = "Bank2"
        //    };
        //    var bank3 = new Bank
        //    {
        //        Name = "Bank1"
        //    };
        //    var sourceList = new List<Bank>
        //    {
        //        bank3
        //    };
        //    var banks = new List<Bank>
        //    {
        //        bank1,
        //        bank2
        //    };

        //    sourceList.IncludeSequence(banks);

        //    Assert.IsNotNull(sourceList);
        //    Assert.AreEqual(2, sourceList.Count);
        //}

        //[TestMethod]
        //public void TestSameSequenceWithDifferenceDepartments()
        //{
        //    var bank1 = new Bank
        //    {
        //        Name = "Bank1",
        //        BankDepartment = new List<BankDepartment>
        //        {
        //            new BankDepartment
        //            {
        //                Address = "Address1",
        //                Name = "Name1"
        //            }
        //        }
        //    };
        //    var bank2 = new Bank
        //    {
        //        Name = "Bank2",
        //        BankDepartment = new List<BankDepartment>
        //        {
        //            new BankDepartment
        //            {
        //                Name = "Name2",
        //                Address = "Address2"
        //            }
        //        }
        //    };
        //    var bank3 = new Bank
        //    {
        //        Name = "Bank1",
        //        BankDepartment = new List<BankDepartment>
        //        {
        //            new BankDepartment
        //            {
        //                Name = "Name3",
        //                Address = "Address3"
        //            }
        //        }
        //    };
        //    var sourceList = new List<Bank>
        //    {
        //        bank3
        //    };
        //    var banks = new List<Bank>
        //    {
        //        bank1,
        //        bank2
        //    };

        //    sourceList.IncludeSequence(banks);

        //    Assert.IsNotNull(sourceList);
        //    Assert.AreEqual(2, sourceList.Count);
        //    Assert.AreEqual(2, bank3.BankDepartment.Count);
        //    Assert.AreEqual(1, bank2.BankDepartment.Count);
        //}

        //[TestMethod]
        //public void TestSameSequenceWithSameDepartments()
        //{
        //    var bank1 = new Bank
        //    {
        //        Name = "Bank1",
        //        BankDepartment = new List<BankDepartment>
        //        {
        //            new BankDepartment
        //            {
        //                Address = "Address1",
        //                Name = "Name1"
        //            }
        //        }
        //    };
        //    var bank2 = new Bank
        //    {
        //        Name = "Bank2",
        //        BankDepartment = new List<BankDepartment>
        //        {
        //            new BankDepartment
        //            {
        //                Name = "Name2",
        //                Address = "Address2"
        //            }
        //        }
        //    };
        //    var bank3 = new Bank
        //    {
        //        Name = "Bank1",
        //        BankDepartment = new List<BankDepartment>
        //        {
        //            new BankDepartment
        //            {
        //                Name = "Name1",
        //                Address = "Address1"
        //            }
        //        }
        //    };
        //    var sourceList = new List<Bank>
        //    {
        //        bank3
        //    };
        //    var banks = new List<Bank>
        //    {
        //        bank1,
        //        bank2
        //    };

        //    sourceList.IncludeSequence(banks);

        //    Assert.IsNotNull(sourceList);
        //    Assert.AreEqual(2, sourceList.Count);
        //    Assert.AreEqual(1, bank3.BankDepartment.Count);
        //    Assert.AreEqual(1, bank2.BankDepartment.Count);
        //}

        //[TestMethod]
        //public void TestSameSequenceWithDepartments()
        //{
        //    var bank1 = new Bank
        //    {
        //        Name = "Bank1",
        //        BankDepartment = new List<BankDepartment>
        //        {
        //            new BankDepartment
        //            {
        //                Address = "Address1",
        //                Name = "Name1"
        //            }
        //        }
        //    };
        //    var bank2 = new Bank
        //    {
        //        Name = "Bank2",
        //        BankDepartment = new List<BankDepartment>
        //        {
        //            new BankDepartment
        //            {
        //                Name = "Name1",
        //                Address = "Address1"
        //            }
        //        }
        //    };
        //    var bank3 = new Bank
        //    {
        //        Name = "Bank1",
        //        BankDepartment = new List<BankDepartment>
        //        {
        //            new BankDepartment
        //            {
        //                Name = "Name3",
        //                Address = "Address3"
        //            }
        //        }
        //    };
        //    var sourceList = new List<Bank>
        //    {
        //        bank3
        //    };
        //    var banks = new List<Bank>
        //    {
        //        bank1,
        //        bank2
        //    };

        //    sourceList.IncludeSequence(banks);

        //    Assert.IsNotNull(sourceList);
        //    Assert.AreEqual(2, sourceList.Count);
        //    Assert.AreEqual(1, bank2.BankDepartment.Count);
        //    Assert.AreEqual(1, bank2.BankDepartment.Count);
        //    Assert.AreEqual(2, bank3.BankDepartment.Count);
        //}

        //#endregion
    }
}
