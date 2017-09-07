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
        public async Task<List<BankDepartment>> Pars(string html, int cityId, int currencyId, DateTime dateTime)
        {
            List<BankDepartment> bankDepartments = new List<BankDepartment>();
            HtmlDocument htmlDocument = new HtmlDocument();
            htmlDocument.LoadHtml(html);

            var trNodes = GetRows(htmlDocument);
            for (var i = 2; i < trNodes.Length; i++)
            {
                var td = GetRowspan(trNodes[i]);

                var purchase = Convert.ToDouble(td[1].FirstChild.InnerHtml.Replace(".", ","));
                var sale = Convert.ToDouble(td[2].FirstChild.InnerHtml.Replace(".", ","));

                var address = td[3].FirstChild.InnerHtml;

                var bankName = td[0].FirstChild.FirstChild.FirstChild.OuterHtml;
                var nameBranch = td[0].Descendants("span").ToArray()[0].InnerHtml;

                BankDepartment bankDepartment = await _bankDepartmentRepository.Find(address, bankName) ?? new BankDepartment
                {
                    Address = address,
                    CityId = cityId,
                    Name = nameBranch,
                    Bank = new Bank
                    {
                        Name = bankName
                    },
                    CurrencyRateByTime = new List<CurrencyRateByTime>()
                };

                var currencyRate = new CurrencyRateByTime
                {
                    CurrencyId = currencyId,
                    DateTime = dateTime,
                    Sale = sale,
                    Purchase = purchase
                };
                bankDepartment.CurrencyRateByTime.Add(currencyRate);
                bankDepartments.Add(bankDepartment);
            }

            return bankDepartments;
        }
        #endregion
        private HtmlNode[] GetRows(HtmlDocument htmlDocument)
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
