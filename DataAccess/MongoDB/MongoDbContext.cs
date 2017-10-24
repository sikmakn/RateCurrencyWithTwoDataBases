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
        }

        private async Task SetIndexes()
        {
        }

        public IMongoCollection<BankDepartmentMongo> BankDepartmentMongoCollection { get; }

        public IMongoCollection<CurrencyRateByTimeMongo> CurrencyRateByTimeMongoCollection { get; }
    }
}
