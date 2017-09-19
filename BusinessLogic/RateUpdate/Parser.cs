using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BusinessLogic.RateUpdate.Interfacies;
using DataAccess.DataBase;
using DataAccess.Repositories.Interfacies;
using HtmlAgilityPack;

namespace BusinessLogic.RateUpdate
{
    public class Parser: IParser
    {
        private readonly IBankDepartmentRepository _bankDepartmentRepository;

        public Parser(IBankDepartmentRepository bankDepartmentRepository)
        {
            _bankDepartmentRepository = bankDepartmentRepository;
        }

        #region IParser
        public bool HasNextPage(string html)
        {
            var htmlDocument = new HtmlDocument();
            htmlDocument.LoadHtml(html);

            var nextPageArrow = htmlDocument.DocumentNode
                .Descendants("a")
                .FirstOrDefault(x => x.Attributes.Contains("title")
                                     && x.Attributes["title"].Value == "Следующая страница");
            return nextPageArrow != null;
        }

        public async Task ParsToIncomingBanks(List<Bank> incomingBanks, string html, int cityId, int currencyId, DateTime dateTime)
        {
            var trNodes = GetTrNodes(html);

            for (var i = 2; i < trNodes.Length; i++)
            {
                var td = GetRowspan(trNodes[i]);

                var purchase = ConvertToDouble(td[1]);
                var sale = ConvertToDouble(td[2]);
                var address = GetAddressFromNode(td[3]);
                var bankName = GetBankNameFromNode(td[0]);
                var departmentName = GetBankDepartmentNameFromNode(td[0]);
           
                var bank = FindOrCreateBank(bankName, incomingBanks);
                var bankDepartment = await FindOrCreateDepartment(address, departmentName, cityId);
                var currencyRate = new CurrencyRateByTime
                {
                    CurrencyId = currencyId,
                    DateTime = dateTime,
                    Sale = sale,
                    Purchase = purchase
                };

                bankDepartment.CurrencyRateByTime.Add(currencyRate);
                bank.BankDepartment.Add(bankDepartment); 
            }
        }
        #endregion

        private async Task<BankDepartment> FindOrCreateDepartment(string address, string departmentName, int cityId)
        {
            return await _bankDepartmentRepository.Find(address, departmentName) ?? new BankDepartment
            {
                Address = address,
                CityId = cityId,
                Name = departmentName,
                CurrencyRateByTime = new List<CurrencyRateByTime>(),
            };
        }

        private static string GetBankDepartmentNameFromNode(HtmlNode node)
        {
            return node.Descendants("span").ToArray()[0].InnerHtml;
        }

        private static string GetBankNameFromNode(HtmlNode node)
        {
            return node.FirstChild.FirstChild.FirstChild.OuterHtml;
        }

        private static string GetAddressFromNode(HtmlNode node)
        {
            return node.FirstChild.InnerHtml;
        }

        private static double ConvertToDouble(HtmlNode node)
        {
            var stingDouble = node.FirstChild.InnerHtml.Replace(".", ",");
            return Convert.ToDouble(stingDouble);
        }

        private static HtmlNode[] GetTrNodes(string html)
        {
            var htmlDocument = new HtmlDocument();
            htmlDocument.LoadHtml(html);
            return GetRows(htmlDocument);
        }

        private static Bank FindOrCreateBank(string bankName, List<Bank> banks)
        {
            var bank = banks.Find(x => x.Name.Contains(bankName));
            if (bank != null) return bank;

            bank = new Bank
            {
                Name = bankName,
                BankDepartment = new List<BankDepartment>()
            };
            banks.Add(bank);
            return bank;
        }

        private static HtmlNode[] GetRows(HtmlDocument htmlDocument)
        {
            var node = htmlDocument
                .DocumentNode
                .Descendants("table")
                .First(x => x.Attributes.Contains("class") &&
                            x.Attributes["class"].Value == "tbl m-tbl");
            var trNodes = node.Descendants("tr").ToArray();
            return trNodes;
        }

        private static HtmlNode[] GetRowspan(HtmlNode tr)
        {
            var td = tr.Descendants("td").ToArray();
            return td;
        }
    }
}
