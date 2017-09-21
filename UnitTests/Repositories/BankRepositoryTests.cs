using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using DataAccess.DataBase;
using DataAccess.Repositories;
using DataAccess.UnitOfWork;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace UnitTests.Repositories
{
    [ExcludeFromCodeCoverage]
    [TestClass]
    public class BankRepositoryTests
    {
        [TestMethod]
        public void TestCorrect()
        {
            var bank1 = new Bank {Name = "Name1"};
            var dataSet = new List<Bank>
            {
                bank1,
                new Bank { Name = "Name2" }
            }.AsQueryable();


            var unitOfWorkMock = UnitOfWorkMock(dataSet);
            var bankRepository = new BankRepository(unitOfWorkMock.Object);

            var resultAwaiter = bankRepository.FindByNameAsync("Name1").GetAwaiter();
            var result = resultAwaiter.GetResult();

            Assert.IsNotNull(result);
            Assert.AreEqual(bank1, result);
        }

        [TestMethod]
        public void TestCorrectWithEmptyResult()
        {
            var bank1 = new Bank { Name = "Name1" };
            var dataSet = new List<Bank>
            {
                bank1,
                new Bank { Name = "Name2" }
            }.AsQueryable();

            var unitOfWorkMock = UnitOfWorkMock(dataSet);
            var bankRepository = new BankRepository(unitOfWorkMock.Object);

            var resultAwaiter = bankRepository.FindByNameAsync("Not have that bank").GetAwaiter();
            var result = resultAwaiter.GetResult();

            Assert.IsNull(result);
        }

        [TestMethod]
        public void TestEmptyInputArgument()
        {
            var bank1 = new Bank { Name = "Name1" };
            var dataSet = new List<Bank>
            {
                bank1,
                new Bank { Name = "Name2" }
            }.AsQueryable();

            var unitOfWorkMock = UnitOfWorkMock(dataSet);
            var bankRepository = new BankRepository(unitOfWorkMock.Object);

            var resultAwaiter = bankRepository.FindByNameAsync("").GetAwaiter();
            var result = resultAwaiter.GetResult();

            Assert.IsNull(result);
        }

        [TestMethod]
        public void TestNullableInputArgument()
        {
            var bank1 = new Bank { Name = "Name1" };
            var dataSet = new List<Bank>
            {
                bank1,
                new Bank { Name = "Name2" }
            }.AsQueryable();

            var unitOfWorkMock = UnitOfWorkMock(dataSet);
            var bankRepository = new BankRepository(unitOfWorkMock.Object);

            var resultAwaiter = bankRepository.FindByNameAsync(null).GetAwaiter();
            var result = resultAwaiter.GetResult();

            Assert.IsNull(result);
        }

        [TestMethod]
        public void TestEmptyDataSetWithNullableInputArgument()
        {
            var dataSet = new List<Bank>().AsQueryable();

            var unitOfWorkMock = UnitOfWorkMock(dataSet);
            var bankRepository = new BankRepository(unitOfWorkMock.Object);

            var resultAwaiter = bankRepository.FindByNameAsync(null).GetAwaiter();
            var result = resultAwaiter.GetResult();

            Assert.IsNull(result);
        }

        [TestMethod]
        public void TestEmptyDataSetWithEmptyInputArgument()
        {
            var dataSet = new List<Bank>().AsQueryable();

            var unitOfWorkMock = UnitOfWorkMock(dataSet);
            var bankRepository = new BankRepository(unitOfWorkMock.Object);

            var resultAwaiter = bankRepository.FindByNameAsync("").GetAwaiter();
            var result = resultAwaiter.GetResult();

            Assert.IsNull(result);
        }

        [TestMethod]
        public void TestEmptyDataSet()
        {
            var dataSet = new List<Bank>().AsQueryable();

            var unitOfWorkMock = UnitOfWorkMock(dataSet);
            var bankRepository = new BankRepository(unitOfWorkMock.Object);

            var resultAwaiter = bankRepository.FindByNameAsync("Name1").GetAwaiter();
            var result = resultAwaiter.GetResult();

            Assert.IsNull(result);
        }

        private static Mock<IUnitOfWork> UnitOfWorkMock(IQueryable<Bank> dataSet)
        {
            var mockSet = new Mock<DbSet<Bank>>();
            mockSet.As<IDbAsyncEnumerable<Bank>>()
                .Setup(m => m.GetAsyncEnumerator())
                .Returns(new TestDbAsyncEnumerator<Bank>(dataSet.GetEnumerator()));

            mockSet.As<IQueryable<Bank>>()
                .Setup(m => m.Provider)
                .Returns(new TestDbAsyncQueryProvider<Bank>(dataSet.Provider));
            mockSet.As<IQueryable<Bank>>().Setup(s => s.Expression).Returns(dataSet.Expression);
            mockSet.As<IQueryable<Bank>>().Setup(s => s.ElementType).Returns(dataSet.ElementType);
            mockSet.As<IQueryable<Bank>>().Setup(s => s.GetEnumerator()).Returns(dataSet.GetEnumerator);


            var mockContext = new Mock<RateCurrencyContext>();
            mockContext.Setup(m => m.Bank).Returns(mockSet.Object);
            mockContext.Setup(x => x.Set<Bank>()).Returns(mockSet.Object);
            var mockUnitOfWork = new Mock<IUnitOfWork>();
            mockUnitOfWork.Setup(s => s.Context).Returns(mockContext.Object);

            return mockUnitOfWork;
        }
    }
}
