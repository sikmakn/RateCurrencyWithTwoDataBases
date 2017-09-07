using System.Collections.Generic;
using DataAccess.DataBase.Interfacies;

namespace DataAccess.Repositories.Interfacies
{
    public interface IDictionaryRepository<T> where T: DBDictionary
    {
        ICollection<T> GetAll();
        T Add(T t);
        T FindByName(string name);
        T AddIfNotHave(T t);
    }
}
