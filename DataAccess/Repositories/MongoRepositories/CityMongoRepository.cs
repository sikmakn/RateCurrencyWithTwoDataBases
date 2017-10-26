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
    public class CityMongoRepository: ICityRepository
    {
        private readonly IMongoCollection<CityMongo> _cityMongoCollection;

        public CityMongoRepository(IUnitOfWork unitOfWork)
        {
            _cityMongoCollection = unitOfWork.MongoContext.CityMongoCollection;
        }

        public async Task<ICollection<CityServiceModel>> GetAll()
        {
            var cityServiceModelList = new List<CityServiceModel>();
            using (var cursor = await _cityMongoCollection.FindAsync(new BsonDocument()))
            {
                while (await cursor.MoveNextAsync())
                {
                    var cities= cursor.Current;
                    cityServiceModelList.AddRange(cities.Select(cityMongo => new CityServiceModel
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
