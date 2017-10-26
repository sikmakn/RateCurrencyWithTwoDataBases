using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace DataAccess.MongoDB.Models
{
    public class CityMongo
    {
        [BsonId]
        public ObjectId Id { get; set; }

        public string Name { get; set; }
    }
}
