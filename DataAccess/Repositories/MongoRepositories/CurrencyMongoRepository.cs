using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DataAccess.ModelsForServices;
using DataAccess.MongoDB.Models;
using DataAccess.Repositories.Interfacies;
using DataAccess.UnitOfWork;
using MongoDB.Bson;
using MongoDB.Driver;

namespace DataAccess.Repositories.MongoRepositories
{
    public class CurrencyMongoRepository: ICurrencyRepository
    {
        private readonly IMongoCollection<CurrencyMongo> _currencyMongoCollection;

        public CurrencyMongoRepository(IUnitOfWork unitOfWork)
        {
            _currencyMongoCollection = unitOfWork.MongoContext.CurrencyMongoCollection;
        }

        public async Task<ICollection<CurrencyServiceModel>> GetAll()
        {
            var cityServiceModelList = new List<CurrencyServiceModel>();
            using (var cursor = await _currencyMongoCollection.FindAsync(new BsonDocument()))
            {
                while (await cursor.MoveNextAsync())
                {
                    var cities = cursor.Current;
                    cityServiceModelList.AddRange(cities.Select(cityMongo => new CurrencyServiceModel
                    {
                        Id = cityMongo.Id.ToString(),
                        Name = cityMongo.Name
                    }));
                }
            }
            return cityServiceModelList;
        }
    }
}
