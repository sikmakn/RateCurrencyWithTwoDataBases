using System.Linq;
using DataAccess.MongoDB.Models;
using DataAccess.Repositories.Interfacies;
using DataAccess.UnitOfWork;
using MongoDB.Bson;
using MongoDB.Driver;

namespace DataAccess.MongoDB.Repositories
{
    public class CurrencyRateByTimeMongoRepository
    {
        private readonly IMongoCollection<CurrencyRateByTimeMongo> _currencyRateMongoCollection;

        public CurrencyRateByTimeMongoRepository(IUnitOfWork unitOfWork)
        {
            _currencyRateMongoCollection = unitOfWork.MongoContext.CurrencyRateByTimeMongoCollection;
        }



    }
}
