using System;
using System.Collections.Generic;
using DataAccess.DataBase;
using DataAccess.ModelsForServices;

namespace BusinessLogic.RateUpdate.Interfacies
{
    public interface IParser
    {
        bool HasNextPage(string html);
        List<BankServiceModel> ParsToIncomingBanks(string html, string cityId, string currencyId, DateTime dateTime);
    }
}
