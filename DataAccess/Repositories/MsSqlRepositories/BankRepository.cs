using System.Collections.Generic;
using System.Data.Entity;
using System.Threading.Tasks;
using DataAccess.DataBase;
using DataAccess.ModelsForServices;
using DataAccess.Repositories.Interfacies;
using DataAccess.UnitOfWork;

namespace DataAccess.Repositories.MsSqlRepositories
{
    public class BankRepository: IBankRepository
    {
        private readonly DbSet<Bank> _banks;

        public BankRepository(IUnitOfWork unitOfWork)
        {
            _banks = unitOfWork.Context.Set<Bank>();
        }

        public BankServiceModel Add(BankServiceModel bank)
        {
           // return _banks.Add(bank);//todo: AddConvert
           return new BankServiceModel();
        }

        public async Task<BankServiceModel> FindByNameAsync(string name)
        {
            var bank = await _banks.FirstOrDefaultAsync(x => x.Name == name);

            var bankServiceModel =  new BankServiceModel
            {
                Id = bank.Id.ToString(),
                Name = bank.Name,
                BankDepartment = new List<BankDepartmentServiceModel>()
            };

            foreach (var bankDepartment in bank.BankDepartment)
            {
                bankServiceModel.BankDepartment.Add(new BankDepartmentServiceModel
                {
                    Id = bankDepartment.Id.ToString(),
                    Address = bankDepartment.Address,
                    Name = bankDepartment.Name,
                    BankId = bankDepartment.BankId.ToString(),
                    CityId = bankDepartment.CityId.ToString(),
                    CurrencyRateByTime = new List<CurrencyRateByTimeServiceModel>()//todo: AddConvertCurrencyRate
                });
            }

            return bankServiceModel;
        }
    }
}
