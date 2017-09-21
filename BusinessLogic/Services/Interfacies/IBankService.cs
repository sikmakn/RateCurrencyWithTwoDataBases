
using System.Collections.Generic;
using System.Threading.Tasks;
using DataAccess.DataBase;

namespace BusinessLogic.Services.Interfacies
{
    public interface IBankService
    {
        Task IncludeSequenceToDataBaseAsync(IEnumerable<Bank> banks);
    }
}
