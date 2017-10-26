using System.Configuration;
using System.Threading.Tasks;
using DataAccess.MongoDB.Models;
using MongoDB.Driver;

namespace DataAccess.MongoDB
{
    public class MongoDbContext
    {
        private readonly string _connectionString = ConfigurationManager.ConnectionStrings["MongoDb"].ConnectionString;
        public  IMongoClient MongoClient { get; }
        public IMongoDatabase CurrencyRateDb { get; }

        public MongoDbContext()
        {
            MongoClient = new MongoClient(_connectionString);
            CurrencyRateDb = MongoClient.GetDatabase("currencyRate");

            BankDepartmentMongoCollection = CurrencyRateDb.GetCollection<BankDepartmentMongo>("BankDepartments");

            CurrencyRateByTimeMongoCollection =
                CurrencyRateDb.GetCollection<CurrencyRateByTimeMongo>("CurrencyRatesByTime");

            CityMongoCollection = CurrencyRateDb.GetCollection<CityMongo>("City");

            CurrencyMongoCollection = CurrencyRateDb.GetCollection<CurrencyMongo>("Currency");

            SetIndexes();
        }

        private async void SetIndexes()
        {
            await CurrencyRateByTimeMongoCollection.Indexes.CreateOneAsync(
                Builders<CurrencyRateByTimeMongo>.IndexKeys.Ascending(x => x.DateTime));
            await CurrencyRateByTimeMongoCollection.Indexes.CreateOneAsync(
                Builders<CurrencyRateByTimeMongo>.IndexKeys.Ascending(x => x.Currency));


            await BankDepartmentMongoCollection.Indexes.CreateOneAsync(
                Builders<BankDepartmentMongo>.IndexKeys.Ascending(x => x.CityName));
        }

        public IMongoCollection<BankDepartmentMongo> BankDepartmentMongoCollection { get; }

        public IMongoCollection<CurrencyRateByTimeMongo> CurrencyRateByTimeMongoCollection { get; }

        public IMongoCollection<CityMongo> CityMongoCollection { get; }

        public IMongoCollection<CurrencyMongo> CurrencyMongoCollection { get; }
    }
}
