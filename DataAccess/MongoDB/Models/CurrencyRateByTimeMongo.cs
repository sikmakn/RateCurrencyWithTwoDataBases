using System;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Driver;

namespace DataAccess.MongoDB.Models
{
    public class CurrencyRateByTimeMongo
    {
        [BsonId]
        public ObjectId Id { get; set; }

        public DateTime DateTime { get; set; }

        public double Sale { get; set; }

        public double Purchase { get; set; }

        public string Currency { get; set; }

        public MongoDBRef BankDepartmentMongo { get; set; }
    }
}
