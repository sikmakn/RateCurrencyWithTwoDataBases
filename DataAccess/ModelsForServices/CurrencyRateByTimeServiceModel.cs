using System;

namespace DataAccess.ModelsForServices
{
    public class CurrencyRateByTimeServiceModel
    {
        public string Id { get; set; }

        public double Purchase { get; set; }

        public double Sale { get; set; }

        public string CurrencyId { get; set; }

        public  DateTime DateTime { get; set; }

        public BankDepartmentServiceModel BankDepartment { get; set; }

        public string BankDepartmentId { get; set; }

        public static CurrencyRateByTimeServiceModel GetNewCurrencyRateByTime(string currencyId, DateTime dateTime, double sale, double purchase)
        {
            return new CurrencyRateByTimeServiceModel
            {
                Sale = sale,
                Purchase = purchase,
                CurrencyId = currencyId,
                DateTime = dateTime,
            };
        }
    }
}
