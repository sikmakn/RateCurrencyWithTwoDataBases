using System.Data.Entity;
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

        public Bank Add(Bank bank)
        {
            return _banks.Add(bank);
        }

        public async Task<Bank> FindByNameAsync(string name)
        {
            return await _banks.FirstOrDefaultAsync(x => x.Name == name);
        }
    }
}
