using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using BusinessLogic.Helpers.Interfacies;
using BusinessLogic.RateUpdate;
using BusinessLogic.RateUpdate.Interfacies;
using BusinessLogic.Services.Interfacies;
using DataAccess.DataBase;
using DataAccess.Repositories.Interfacies;
using DataAccess.UnitOfWork;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace UnitTests.RateUpdateTests
{
    [ExcludeFromCodeCoverage]
    [TestClass]
    public class RateUpdaterTests
    {
        //[TestMethod]
        //public void TestDifferenceBanks()
        //{
        //    var cityRepositoryMock = CityRepositoryMock();
        //    var currencyRepositoryMock = CurrencyRepositoryMock();
        //    var unitOfWorkMock = UnitOfWorkMock();
        //    var readerMock = ReaderMock();

        //    IEnumerable<Bank> result = new List<Bank>();
        //    var bankServiceMock = new Mock<IBankService>();
        //    bankServiceMock.Setup(x => x.IncludeSequenceToDataBaseAsync(It.IsAny<IEnumerable<Bank>>()))
        //        .Callback((IEnumerable<Bank> b) => { result = b; });

        //    var parserResult1 = new List<Bank>
        //    {
        //        new Bank
        //        {
        //            Name = "Bank1",
        //            BankDepartment = new List<BankDepartment>
        //            {
        //                new BankDepartment
        //                {
        //                    Address = "Address1",
        //                    Name = "Department1",
        //                    CurrencyRateByTime = new List<CurrencyRateByTime>
        //                    {
        //                        new CurrencyRateByTime
        //                        {
        //                            CurrencyId = 1,
        //                            Sale = 1,
        //                            Purchase = 1
        //                        }
        //                    }
        //                }
        //            }
        //        }
        //    };
        //    var parserResult2 = new List<Bank>
        //    {
        //        new Bank
        //        {
        //            Name = "Bank2",
        //            BankDepartment = new List<BankDepartment>
        //            {
        //                new BankDepartment
        //                {
        //                    Address = "Address2",
        //                    Name = "Department2",
        //                    CurrencyRateByTime = new List<CurrencyRateByTime>
        //                    {
        //                        new CurrencyRateByTime
        //                        {
        //                            CurrencyId = 2,
        //                            Sale = 2,
        //                            Purchase = 2
        //                        }
        //                    }
        //                }
        //            }
        //        }
        //    };
        //    var parserMock = ParserMock(parserResult1, parserResult2);

        //    var rateUpdater = new RateUpdater(cityRepositoryMock.Object, currencyRepositoryMock.Object, parserMock.Object, 
        //        readerMock.Object, bankServiceMock.Object, unitOfWorkMock.Object);

        //    rateUpdater.Update().GetAwaiter();

        //    Assert.IsNotNull(result);
        //    Assert.AreEqual(2, result.Count());
        //    Assert.AreEqual(parserResult1[0].BankDepartment.Count, result.FirstOrDefault().BankDepartment.Count);
        //    Assert.AreEqual(parserResult2[0].BankDepartment.Count, result.LastOrDefault().BankDepartment.Count);
        //}


        //[TestMethod]
        //public void TestSamebanksAndDifferenceDepartments()
        //{
        //    var cityRepositoryMock = CityRepositoryMock();
        //    var currencyRepositoryMock = CurrencyRepositoryMock();
        //    var unitOfWorkMock = UnitOfWorkMock();
        //    var readerMock = ReaderMock();

        //    IEnumerable<Bank> result = new List<Bank>();
        //    var bankServiceMock = new Mock<IBankService>();
        //    bankServiceMock.Setup(x => x.IncludeSequenceToDataBaseAsync(It.IsAny<IEnumerable<Bank>>()))
        //        .Callback((IEnumerable<Bank> b) => { result = b; });

        //    var parserResult1 = new List<Bank>
        //    {
        //        new Bank
        //        {
        //            Name = "Bank1",
        //            BankDepartment = new List<BankDepartment>
        //            {
        //                new BankDepartment
        //                {
        //                    Address = "Address1",
        //                    Name = "Department1",
        //                    CurrencyRateByTime = new List<CurrencyRateByTime>
        //                    {
        //                        new CurrencyRateByTime
        //                        {
        //                            CurrencyId = 1,
        //                            Sale = 1,
        //                            Purchase = 1
        //                        }
        //                    }
        //                }
        //            }
        //        }
        //    };
        //    var parserResult2 = new List<Bank>
        //    {
        //        new Bank
        //        {
        //            Name = "Bank1",
        //            BankDepartment = new List<BankDepartment>
        //            {
        //                new BankDepartment
        //                {
        //                    Address = "Address2",
        //                    Name = "Department2",
        //                    CurrencyRateByTime = new List<CurrencyRateByTime>
        //                    {
        //                        new CurrencyRateByTime
        //                        {
        //                            CurrencyId = 2,
        //                            Sale = 2,
        //                            Purchase = 2
        //                        }
        //                    }
        //                }
        //            }
        //        }
        //    };
        //    var parserMock = ParserMock(parserResult1, parserResult2);

        //    var rateUpdater = new RateUpdater(cityRepositoryMock.Object, currencyRepositoryMock.Object, parserMock.Object,
        //        readerMock.Object, bankServiceMock.Object, unitOfWorkMock.Object);

        //    rateUpdater.Update().GetAwaiter();

        //    Assert.IsNotNull(result);
        //    Assert.AreEqual(1, result.Count());
        //    Assert.AreEqual(2, result.FirstOrDefault().BankDepartment.Count);
        //}

        //[TestMethod]
        //public void TestSamebanksAndSameDepartments()
        //{
        //    var cityRepositoryMock = CityRepositoryMock();
        //    var currencyRepositoryMock = CurrencyRepositoryMock();
        //    var unitOfWorkMock = UnitOfWorkMock();
        //    var readerMock = ReaderMock();

        //    IEnumerable<Bank> result = new List<Bank>();
        //    var bankServiceMock = new Mock<IBankService>();
        //    bankServiceMock.Setup(x => x.IncludeSequenceToDataBaseAsync(It.IsAny<IEnumerable<Bank>>()))
        //        .Callback((IEnumerable<Bank> b) => { result = b; });

        //    var parserResult1 = new List<Bank>
        //    {
        //        new Bank
        //        {
        //            Name = "Bank1",
        //            BankDepartment = new List<BankDepartment>
        //            {
        //                new BankDepartment
        //                {
        //                    Address = "Address1",
        //                    Name = "Department1",
        //                    CurrencyRateByTime = new List<CurrencyRateByTime>
        //                    {
        //                        new CurrencyRateByTime
        //                        {
        //                            CurrencyId = 1,
        //                            Sale = 1,
        //                            Purchase = 1
        //                        }
        //                    }
        //                }
        //            }
        //        }
        //    };
        //    var parserResult2 = new List<Bank>
        //    {
        //        new Bank
        //        {
        //            Name = "Bank1",
        //            BankDepartment = new List<BankDepartment>
        //            {
        //                new BankDepartment
        //                {
        //                    Address = "Address1",
        //                    Name = "Department1",
        //                    CurrencyRateByTime = new List<CurrencyRateByTime>
        //                    {
        //                        new CurrencyRateByTime
        //                        {
        //                            CurrencyId = 2,
        //                            Sale = 2,
        //                            Purchase = 2
        //                        }
        //                    }
        //                }
        //            }
        //        }
        //    };
        //    var parserMock = ParserMock(parserResult1, parserResult2);

        //    var rateUpdater = new RateUpdater(cityRepositoryMock.Object, currencyRepositoryMock.Object, parserMock.Object,
        //        readerMock.Object, bankServiceMock.Object, unitOfWorkMock.Object);

        //    rateUpdater.Update().GetAwaiter();

        //    Assert.IsNotNull(result);
        //    Assert.AreEqual(1, result.Count());
        //    Assert.AreEqual(1, result.FirstOrDefault().BankDepartment.Count);
        //    Assert.AreEqual(2, result.FirstOrDefault().BankDepartment.FirstOrDefault().CurrencyRateByTime.Count);
        //}

        //#region InitializeMocks

        //private static Mock<IParser> ParserMock(List<Bank> result1, List<Bank> result2)
        //{
        //    var parserMock = new Mock<IParser>();
        //    parserMock.Setup(s => s.HasNextPage(It.IsAny<string>())).Returns(false);
        //    parserMock.Setup(s =>
        //            s.ParsToIncomingBanks(It.IsAny<string>(), It.IsAny<int>(), It.IsAny<int>(), It.IsAny<DateTime>()))
        //        .Returns((string h, int cityId, int currencyId, DateTime d) => currencyId == 1 ? result1 : result2);
        //    return parserMock;
        //}
  
        //private static Mock<IReader> ReaderMock()
        //{
        //    const string html = "some html";
        //    var readerMock = new Mock<IReader>();
        //    readerMock.Setup(x => x.HttpClientRead(It.IsAny<string>())).ReturnsAsync(html);
        //    return readerMock;
        //}
        //private static Mock<IUnitOfWork> UnitOfWorkMock()
        //{
        //    var unitOfWorkMock = new Mock<IUnitOfWork>();
        //    unitOfWorkMock.Setup(x => x.SaveChangesAsync()).ReturnsAsync(1);
        //    return unitOfWorkMock;
        //}
        //private static Mock<IDictionaryRepository<Currency>> CurrencyRepositoryMock()
        //{
        //    var currencyRepository = new Mock<IDictionaryRepository<Currency>>();
        //    var currencies = new List<Currency>
        //    {
        //        new Currency
        //        {
        //            Id = 1,
        //            Name = "dollar"
        //        },
        //        new Currency
        //        {
        //            Id = 2,
        //            Name = "euro"
        //        },
        //    };
        //    currencyRepository.Setup(x => x.GetAll()).Returns(currencies);
        //    return currencyRepository;
        //}

        //private static Mock<IDictionaryRepository<City>> CityRepositoryMock()
        //{
        //    var cityRepository =  new Mock<IDictionaryRepository<City>>();
        //    var cities = new List<City>
        //    {
        //        new City
        //        {
        //            Id = 1,
        //            Name = "minsk"
        //        },
        //    };
        //    cityRepository.Setup(x => x.GetAll()).Returns(cities);
        //    return cityRepository;
        //}

        //#endregion
    }
}
