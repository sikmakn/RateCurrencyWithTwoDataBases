using System;
using System.Collections.Generic;
using System.Linq;
using BusinessLogic.RateUpdate.Interfacies;
using DataAccess.DataBase;
using DataAccess.DataBase.ModelsHelpers;
using HtmlAgilityPack;

namespace BusinessLogic.RateUpdate
{
    public class Parser: IParser
    {
        #region IParser
        public bool HasNextPage(string html)
        {
            var htmlDocument = CreateHtmlDocument(html);

            var nextPageArrow = htmlDocument.DocumentNode
                .Descendants("a")
                .FirstOrDefault(x => x.Attributes.Contains("title")
                                     && x.Attributes["title"].Value == "Следующая страница");
            return nextPageArrow != null;
        }

        public List<Bank> ParsToIncomingBanks(string html, int cityId, int currencyId, DateTime dateTime)
        {
            var trNodes = GetTrNodes(html);

            var banks = new List<Bank>();
            for (var i = 2; i < trNodes.Length; i++)
            {
                var td = GetRowspan(trNodes[i]);

                var purchase = ConvertToDouble(td[1]);
                var sale = ConvertToDouble(td[2]);
                var address = GetAddressFromNode(td[3]);
                var bankName = GetBankNameFromNode(td[0]);
                var departmentName = GetBankDepartmentNameFromNode(td[0]);

                var bank = FindOrCreateBank(bankName, banks);
                var bankDepartment = BankDepartmentHelper.GetNewBankDepartment(address, departmentName, cityId);
                var currencyRate = CurrencyRateByTime.GetNewCurrencyRateByTime(currencyId, dateTime, sale, purchase);

                bankDepartment.CurrencyRateByTime.Add(currencyRate);
                bank.BankDepartment.Add(bankDepartment);
            }
            return banks;
        }
        #endregion

        private static HtmlDocument CreateHtmlDocument(string html)
        {
            if (html == null) throw new NullReferenceException("Nullable html string");

            var htmlDocument = new HtmlDocument();
            htmlDocument.LoadHtml(html);
            return htmlDocument;
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
            var htmlDocument = CreateHtmlDocument(html);
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
                .FirstOrDefault(x => x.Attributes.Contains("class") &&
                            x.Attributes["class"].Value == "tbl m-tbl");
            var trNodes = node?.Descendants("tr").ToArray();
            return trNodes ?? new HtmlNode[0];
        }

        private static HtmlNode[] GetRowspan(HtmlNode tr)
        {
            var td = tr?.Descendants("td").ToArray();
            return td ?? new HtmlNode[0];
        }

    }
}
