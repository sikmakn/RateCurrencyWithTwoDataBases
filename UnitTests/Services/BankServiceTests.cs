using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using BusinessLogic.Services;
using DataAccess.DataBase;
using DataAccess.Repositories.Interfacies;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace UnitTests.Services
{
    [ExcludeFromCodeCoverage]
    [TestClass]
    public class BankServiceTests
    {

        //#region IncludeSequenceToDataBase

        //[TestMethod]
        //public void TestDifferenceBanks()
        //{
        //    var banks = new Dictionary<string, Bank>
        //    {
        //        {
        //            "Bank1", new Bank
        //            {
        //                Name = "Bank1",
        //                BankDepartment = new List<BankDepartment>
        //                {
        //                    new BankDepartment
        //                    {
        //                        Address = "departmentAddress1",
        //                        Name = "departmentName1"
        //                    }
        //                }
        //            }
        //        }
        //    };

        //    var bankReposotitoryMock = BankRepositoryMock(banks);

        //    var inputbanks = new List<Bank>
        //    {
        //        new Bank
        //        {
        //            Name = "Bank2",
        //            BankDepartment = new List<BankDepartment>
        //            {
        //                new BankDepartment
        //                {
        //                    Address = "departmentAddress2",
        //                    Name = "departmentName2"
        //                }
        //            }
        //        }
        //    };

        //    var bankService = new BankService(bankReposotitoryMock.Object);

        //    bankService.IncludeSequenceToDataBaseAsync(inputbanks).GetAwaiter();

        //    Assert.IsNotNull(banks);
        //    Assert.AreEqual(2, banks.Count);
        //    Assert.AreEqual(1, banks["Bank1"].BankDepartment.Count);
        //    Assert.AreEqual(1, banks["Bank2"].BankDepartment.Count);
        //}

        //[TestMethod]
        //public void TestSameBanksWithDifferenceDepartments()
        //{
        //    var banks = new Dictionary<string, Bank>
        //    {
        //        {
        //            "Bank1", new Bank
        //            {
        //                Name = "Bank1",
        //                BankDepartment = new List<BankDepartment>
        //                {
        //                    new BankDepartment
        //                    {
        //                        Address = "departmentAddress1",
        //                        Name = "departmentName1"
        //                    }
        //                }
        //            }
        //        }
        //    };

        //    var bankReposotitoryMock = BankRepositoryMock(banks);

        //    var inputbanks = new List<Bank>
        //    {
        //        new Bank
        //        {
        //            Name = "Bank1",
        //            BankDepartment = new List<BankDepartment>
        //            {
        //                new BankDepartment
        //                {
        //                    Address = "departmentAddress2",
        //                    Name = "departmentName2"
        //                }
        //            }
        //        }
        //    };

        //    var bankService = new BankService(bankReposotitoryMock.Object);

        //    bankService.IncludeSequenceToDataBaseAsync(inputbanks).GetAwaiter();

        //    Assert.IsNotNull(banks);
        //    Assert.AreEqual(1, banks.Count);
        //    Assert.AreEqual(2, banks["Bank1"].BankDepartment.Count);
        //}

        //[TestMethod]
        //public void TestEmptyDb()
        //{
        //    var banks = new Dictionary<string, Bank>();

        //    var bankReposotitoryMock = BankRepositoryMock(banks);

        //    var inputbanks = new List<Bank>
        //    {
        //        new Bank
        //        {
        //            Name = "Bank1",
        //            BankDepartment = new List<BankDepartment>
        //            {
        //                new BankDepartment
        //                {
        //                    Address = "departmentAddress2",
        //                    Name = "departmentName2"
        //                }
        //            }
        //        }
        //    };

        //    var bankService = new BankService(bankReposotitoryMock.Object);

        //    bankService.IncludeSequenceToDataBaseAsync(inputbanks).GetAwaiter();

        //    Assert.IsNotNull(banks);
        //    Assert.AreEqual(1, banks.Count);
        //    Assert.AreEqual(1, banks["Bank1"].BankDepartment.Count);
        //}

        //[TestMethod]
        //public void TestEmptyInputSequence()
        //{
        //    var banks = new Dictionary<string, Bank>
        //    {
        //        {
        //            "Bank1", new Bank
        //            {
        //                Name = "Bank1",
        //                BankDepartment = new List<BankDepartment>
        //                {
        //                    new BankDepartment
        //                    {
        //                        Address = "departmentAddress1",
        //                        Name = "departmentName1"
        //                    }
        //                }
        //            }
        //        }
        //    };

        //    var bankReposotitoryMock = BankRepositoryMock(banks);

        //    var inputbanks = new List<Bank>();

        //    var bankService = new BankService(bankReposotitoryMock.Object);

        //    bankService.IncludeSequenceToDataBaseAsync(inputbanks).GetAwaiter();

        //    Assert.IsNotNull(banks);
        //    Assert.AreEqual(1, banks.Count);
        //    Assert.AreEqual(1, banks["Bank1"].BankDepartment.Count);
        //}

        //[TestMethod]
        //public void TestNullableInputSequence()
        //{
        //    var banks = new Dictionary<string, Bank>
        //    {
        //        {
        //            "Bank1", new Bank
        //            {
        //                Name = "Bank1",
        //                BankDepartment = new List<BankDepartment>
        //                {
        //                    new BankDepartment
        //                    {
        //                        Address = "departmentAddress1",
        //                        Name = "departmentName1"
        //                    }
        //                }
        //            }
        //        }
        //    };

        //    var bankReposotitoryMock = BankRepositoryMock(banks);
            
        //    var bankService = new BankService(bankReposotitoryMock.Object);

        //    bankService.IncludeSequenceToDataBaseAsync(null).GetAwaiter();
        //    Assert.AreEqual(1, banks.Count);
        //    Assert.AreEqual(1, banks["Bank1"].BankDepartment.Count);
        //}
        //#endregion

        //private static Mock<IBankRepository> BankRepositoryMock(IDictionary<string, Bank> banks)
        //{
        //    var bankReposotitory = new Mock<IBankRepository>();
        //    var bankId = 0;
        //    var departmentId = 0;
        //    bankReposotitory.Setup(x => x.Add(It.IsAny<Bank>())).Returns((Bank b) =>
        //    {
        //        b.Id = bankId;
        //        bankId++;
        //        foreach (var department in b.BankDepartment)
        //        {
        //            department.BankId = b.Id;
        //            department.Bank = b;
        //            foreach (var rateByTime in department.CurrencyRateByTime)
        //            {
        //                rateByTime.BankDepartment = department;
        //                rateByTime.BankDepartmentId = departmentId;
        //                departmentId++;
        //            }
        //        }
        //        banks.Add(b.Name, b);
        //        return b;
        //    });

        //    bankReposotitory.Setup(x => x.FindByNameAsync(It.IsAny<string>())).ReturnsAsync((string key) => banks.FirstOrDefault(x => x.Key == key).Value);
        //    return bankReposotitory;
        //}
    }
}
