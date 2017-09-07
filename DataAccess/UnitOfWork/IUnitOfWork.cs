using System.Threading.Tasks;
using DataAccess.DataBase;

namespace DataAccess.UnitOfWork
{
    public interface IUnitOfWork
    {
        RateCurrencyContext Context { get; }

        int SaveChanges();

        Task<int> SaveChangesAsync();

        void Dispose();
    }
}
