using DataAccess.MongoDB.Models;
using DataAccess.UnitOfWork;
using MongoDB.Driver;

namespace DataAccess.MongoDB.Repositories
{
    public class BankDepartmentMongoRepository
    {
        private IMongoCollection<BankDepartmentMongo> _bankDepartmentMongoCollection;

        public BankDepartmentMongoRepository(IUnitOfWork unitOfWork)
        {
            _bankDepartmentMongoCollection = unitOfWork.MongoContext.BankDepartmentMongoCollection;
        }
    }
}
