using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using DataAccess.DataBase.Interfacies;
using DataAccess.Repositories.Interfacies;
using DataAccess.UnitOfWork;

namespace DataAccess.Repositories
{
    public class DictionaryRepository<T>: IDictionaryRepository<T> where T: DBDictionary 
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

        public T AddIfNotHave(T t)
        {
            var value = FindByName(t.Name);
            if (value == null) return Add(t);
            _dbSet.Attach(value);
            return value;
        }

        public T Add(T t)
        {
            return _dbSet.Add(t);
        }

        public T FindByName(string name)
        {
            return _dbSet.FirstOrDefault(x => x.Name == name);
        }
    }
}