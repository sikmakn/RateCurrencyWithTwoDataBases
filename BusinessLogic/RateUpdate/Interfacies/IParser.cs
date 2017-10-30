using System;
using System.Collections.Generic;
using DataAccess.DataBase;
using DataAccess.ModelsForServices;

namespace BusinessLogic.RateUpdate.Interfacies
{
    public interface IParser
    {
        bool HasNextPage(string html);

        List<CurrencyRateByTimeServiceModel> ParsToCurrenciesRatesByTimes(string html, CityServiceModel city,
            CurrencyServiceModel currency, DateTime dateTime);
    }
}
