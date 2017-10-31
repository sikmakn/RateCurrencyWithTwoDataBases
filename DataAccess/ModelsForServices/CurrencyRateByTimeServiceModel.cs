using System;

namespace DataAccess.ModelsForServices
{
    public class CurrencyRateByTimeServiceModel
    {
        public string Id { get; set; }

        public double Purchase { get; set; }

        public double Sale { get; set; }

        public CurrencyServiceModel Currency { get; set; }

        public  DateTime DateTime { get; set; }

        public string BankDepartmentId { get; set; }

    }
}
