using System.Collections.Generic;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Driver;

namespace DataAccess.MongoDB.Models
{
    public class BankDepartmentMongo
    {
        [BsonId]
        public ObjectId Id { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string BankName { get; set; }
        public string CityName { get; set; }
        public IFilteredMongoCollection<MongoDBRef> CurrencyRateByTime { get; set; }
    }
}
