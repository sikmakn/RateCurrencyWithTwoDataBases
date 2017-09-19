using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using DataAccess.Repositories.Interfacies;
using DataAccess.UnitOfWork;

namespace DataAccess.Repositories
{
    public class DictionaryRepository<T>: IDictionaryRepository<T> where T: class 
    {
        private readonly DbSet<T> _dbSet;

        public DictionaryRepository(IUnitOfWork unitOfWork)
        {
            _dbSet = unitOfWork.Context.Set<T>();
        }

        public ICollection<T> GetAll()
        {
            return _dbSet.ToList();
        }
    }
}