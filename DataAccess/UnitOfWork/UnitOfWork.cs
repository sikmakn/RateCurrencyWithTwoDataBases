using System;
using System.Threading.Tasks;
using DataAccess.DataBase;
using DataAccess.MongoDB;

namespace DataAccess.UnitOfWork
{
    public class UnitOfWork: IUnitOfWork, IDisposable
    {
        public RateCurrencyContext Context { get; } = new RateCurrencyContext();
        public MongoDbContext MongoContext { get; } = new MongoDbContext();

        public int SaveChanges()
        {
            return Context.SaveChanges();
        }

        public async Task<int> SaveChangesAsync()
        {
            return await Context.SaveChangesAsync();
        }

        private bool _disposed;

        public virtual void Dispose(bool disposing)
        {
            if (!_disposed && disposing)
            {
                Context.Dispose();
            }
            _disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}