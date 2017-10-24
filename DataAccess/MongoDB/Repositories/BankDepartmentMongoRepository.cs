using DataAccess.MongoDB.Models;
using DataAccess.UnitOfWork;
using MongoDB.Driver;

namespace DataAccess.MongoDB.Repositories
{
    public class BankDepartmentMongoRepository
    {
        private IMongoCollection<BankDepartmentMongo> _bankDepatmentMongoCollection;

        public BankDepartmentMongoRepository(IUnitOfWork unitOfWork)
        {
            _bankDepatmentMongoCollection = unitOfWork.MongoContext.BankDepartmentMongoCollection;
        }


    }
}
