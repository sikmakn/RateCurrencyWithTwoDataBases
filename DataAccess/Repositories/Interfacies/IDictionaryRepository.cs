using System.Collections.Generic;

namespace DataAccess.Repositories.Interfacies
{
    public interface IDictionaryRepository<T> where T: class 
    {
        ICollection<T> GetAll();
    }
}
