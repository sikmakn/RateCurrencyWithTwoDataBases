using DataAccess.DataBase;

namespace DataAccess.Repositories.Interfacies
{
    public interface IBankRepository
    {
        Bank Add(Bank bank);
        Bank FindByName(string name);
    }
}
