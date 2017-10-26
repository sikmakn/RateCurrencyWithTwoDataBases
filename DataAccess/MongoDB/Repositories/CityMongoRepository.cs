using System.Collections.Generic;
using DataAccess.MongoDB.Models;
using DataAccess.Repositories.Interfacies;
using DataAccess.UnitOfWork;
using MongoDB.Bson;
using MongoDB.Driver;

namespace DataAccess.MongoDB.Repositories
{
    public class CityMongoRepository: IDictionaryRepository<CityMongo>
    {
        private readonly IMongoCollection<CityMongo> _cityCollection;

        public CityMongoRepository(IUnitOfWork unitOfWork)
        {
            _cityCollection = unitOfWork.MongoContext.CityMongoCollection;
        }

        public ICollection<CityMongo> GetAll()
        {
            return _cityCollection.Find(new BsonDocument()).ToList();
        }
    }
}
