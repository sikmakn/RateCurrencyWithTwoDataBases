using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using BusinessLogic.NbrbApiAccess.Models;

namespace BusinessLogic.NbrbApiAccess.Services.Interfacies
{
    public interface IRateNbrbService
    {
        Task<IEnumerable<RateShortNbrb>> ReadCurrenciesNbrb(int currencyId, DateTime startDate, DateTime endDate);
    }
}
