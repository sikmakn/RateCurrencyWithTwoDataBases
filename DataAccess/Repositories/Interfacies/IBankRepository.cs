using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccess.DataBase;

namespace DataAccess.Repositories.Interfacies
{
    public interface IBankRepository
    {
        ICollection<Bank> GetAll();
        Bank Add(Bank bank);
        Bank FindByName(string name);
        Bank AddIfNotHave(Bank bank);
        Task<Bank> FindByIdAsync(int id);
    }
}
