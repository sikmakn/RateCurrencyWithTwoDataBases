using System;
using System.Data.Entity;
using System.Linq;
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
