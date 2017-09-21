using System.Collections.Generic;
using System.Threading.Tasks;
using BusinessLogic.Services.Interfacies;
using DataAccess.DataBase;
using DataAccess.DataBase.ModelsHelpers;
using DataAccess.Repositories.Interfacies;

namespace BusinessLogic.Services
{
    public class BankService: IBankService
    {
        private readonly IBankRepository _bankRepository;

        public BankService(IBankRepository bankRepository)
        {
            _bankRepository = bankRepository;
        }

        public async Task IncludeSequenceToDataBaseAsync(IEnumerable<Bank> banks)
        {
            foreach (var bank in banks)
            {
                var oldBank = await _bankRepository.FindByNameAsync(bank.Name);
                if (oldBank == null)
                {
                    _bankRepository.Add(bank);
                }
                else
                {
                    oldBank.BankDepartment.IncludeSequence(bank.BankDepartment);
                }
            }
        }
    }
}
