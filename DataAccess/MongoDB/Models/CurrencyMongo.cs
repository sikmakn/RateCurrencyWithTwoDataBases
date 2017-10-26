using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace DataAccess.MongoDB.Models
{
    public class CurrencyMongo
    {
        [BsonId]
        public ObjectId Id { get; set; }

        public string Name { get; set; }
    }
}
