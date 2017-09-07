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
        private readonly DbSet<T> _t;

        public DictionaryRepository(IUnitOfWork unitOfWork)
        {
            _t = unitOfWork.Context.Set<T>();
        }

        public ICollection<T> GetAll()
        {
            return _t.ToList();
        }

        public T Add(T t)
        {
            return _t.Add(t);
        }

        public T FindByName(string name)
        {
            return _t.FirstOrDefault(x => x.Name == name);
        }
    }
}