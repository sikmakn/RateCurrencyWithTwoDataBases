
using System.Collections.Generic;
using System.Threading.Tasks;
using DataAccess.DataBase;
using DataAccess.ModelsForServices;

namespace BusinessLogic.Services.Interfacies
{
    public interface IBankService
    {
        Task IncludeSequenceToDataBaseAsync(IEnumerable<BankServiceModel> banks);
    }
}
