
using System.Collections.Generic;
using DataAccess.DataBase;

namespace BusinessLogic.Services.Interfacies
{
    public interface IBankService
    {
        void IncludeSequenceToDataBase(IEnumerable<Bank> banks);
    }
}
