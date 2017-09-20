using System;
using System.Collections.Generic;
using DataAccess.DataBase;

namespace BusinessLogic.RateUpdate.Interfacies
{
    public interface IParser
    {
        bool HasNextPage(string html);
        List<Bank> ParsToIncomingBanks(string html, int cityId, int currencyId, DateTime dateTime);
    }
}
