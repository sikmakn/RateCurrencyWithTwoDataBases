
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using DataAccess.DataBase;
using HtmlAgilityPack;

namespace BusinessLogic.RateUpdate.Interfacies
{
    public interface IParser
    {
        bool HasNextPage(string html);
        Task<List<BankDepartment>> Pars(string html, int cityId, int currencyId, DateTime dateTime);
    }
}
