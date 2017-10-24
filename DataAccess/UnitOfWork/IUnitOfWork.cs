using System.Threading.Tasks;
using DataAccess.DataBase;
using DataAccess.MongoDB;

namespace DataAccess.UnitOfWork
{
    public interface IUnitOfWork
    {
        RateCurrencyContext Context { get; }

        MongoDbContext MongoContext { get; }

        int SaveChanges();

        Task<int> SaveChangesAsync();

        void Dispose();
    }
}
