using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using DataAccess.DataBase;
using DataAccess.Repositories.Interfacies;
using DataAccess.UnitOfWork;

namespace DataAccess.Repositories
{
    public class BankRepository: IBankRepository
    {
        private readonly DbSet<Bank> _banks;

        public BankRepository(IUnitOfWork unitOfWork)
        {
            _banks = unitOfWork.Context.Set<Bank>();
        }

        public ICollection<Bank> GetAll()
        {
            return _banks.ToList();
        }

        public async Task<Bank> FindByIdAsync(int id)
        {
            return await _banks.FindAsync(id);
        }

        public Bank AddIfNotHave(Bank bank)
        {
            var value = FindByName(bank.Name);
            if (value == null) return Add(bank);
            _banks.Attach(value);
            return value;
        }

        public Bank Add(Bank bank)
        {
            try
            {
                return _banks.Add(bank);
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public Bank FindByName(string name)
        {
            return _banks.FirstOrDefault(x => x.Name == name);
        }
    }
}
