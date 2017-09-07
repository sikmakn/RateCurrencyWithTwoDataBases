using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using DataAccess.DataBase;

namespace BusinessLogic.RateUpdate.Interfacies
{
    public interface IParser
    {
        bool HasNextPage(string html);
        Task ParsToIncomingBanks(List<Bank> incomingBanks, string html, int cityId, int currencyId, DateTime dateTime);
    }
}
