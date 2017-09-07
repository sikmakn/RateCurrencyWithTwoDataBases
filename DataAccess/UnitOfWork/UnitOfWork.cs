using System;
using System.Data.Entity;
using System.Runtime.Remoting.Contexts;
using System.Threading.Tasks;
using DataAccess.DataBase;
using DataAccess.DataBase.Interfacies;

namespace DataAccess.UnitOfWork
{
    public class UnitOfWork: IUnitOfWork, IDisposable
    {
        public RateCurrencyContext Context { get; } = new RateCurrencyContext();

        public int SaveChanges()
        {
            return Context.SaveChanges();
        }

        public async Task<int> SaveChangesAsync()
        {
            return await Context.SaveChangesAsync();
        }

        private bool _disposed = false;

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