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
    public class CurrencyRateByTimeMongoRepository: ICurrencyRateByTimeRepository
    {
        private readonly IMongoCollection<CurrencyRateByTimeMongo> _currencyRateMongoCollection;

        public CurrencyRateByTimeMongoRepository(IUnitOfWork unitOfWork)
        {
            _currencyRateMongoCollection = unitOfWork.MongoContext.CurrencyRateByTimeMongoCollection;
        }


        public async Task<IQueryable<CurrencyRateByTimeServiceModel>> GetAll()
        {
            var currencyRateServiceList = new List<CurrencyRateByTimeServiceModel>();
            using (var cursor = await _currencyRateMongoCollection.FindAsync(new BsonDocument()))
            {
                while (await cursor.MoveNextAsync())
                {
                    var currencyRates = cursor.Current;
                    currencyRateServiceList.AddRange(currencyRates.Select(currencyRateByTimeMongo => new CurrencyRateByTimeServiceModel
                    {
                        Id = currencyRateByTimeMongo.Id.ToString(),
                        Sale = currencyRateByTimeMongo.Sale,
                        Purchase = currencyRateByTimeMongo.Purchase,
                        DateTime = currencyRateByTimeMongo.DateTime,
                        //todo: AddRead
                        // BankDepartment = currencyRateByTimeMongo.BankDepartmentMongo.
                        //CurrencyId = 
                    }));
                }
            }
            
            return currencyRateServiceList.AsQueryable();
        }

        public async Task<CurrencyRateByTimeServiceModel> GetById(string id)
        {
            var filter = new BsonDocument("_id", id);
            var currencyRateMongo = (await _currencyRateMongoCollection.FindAsync(filter)).First();
            return new CurrencyRateByTimeServiceModel
            {
                DateTime = currencyRateMongo.DateTime,
                Id = currencyRateMongo.Id.ToString(),
                Sale = currencyRateMongo.Sale,
                Purchase = currencyRateMongo.Purchase,
                //todo: AddRead
                // BankDepartment = currencyRateByTimeMongo.BankDepartmentMongo.
                //CurrencyId = 
            };
        }

    }
}