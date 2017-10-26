using System.Collections.Generic;
using DataAccess.MongoDB.Models;
using DataAccess.Repositories.Interfacies;
using DataAccess.UnitOfWork;
using MongoDB.Bson;
using MongoDB.Driver;

namespace DataAccess.MongoDB.Repositories
{
    public class CurrencyMongoRepository: IDictionaryRepository<CurrencyMongo>
    {
        private readonly IMongoCollection<CurrencyMongo> _currencyCollection;

        public CurrencyMongoRepository(IUnitOfWork unitOfWork)
        {
            _currencyCollection = unitOfWork.MongoContext.CurrencyMongoCollection;
        }

        public ICollection<CurrencyMongo> GetAll()
        {
            return _currencyCollection.Find(new BsonDocument()).ToList();
        }
    }
}
