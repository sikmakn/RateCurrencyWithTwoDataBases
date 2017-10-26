using System.Collections.Generic;

namespace DataAccess.ModelsForServices
{
    public class BankDepartmentServiceModel
    {
        public string Id { get; set; }

        public string Name { get; set; }

        public string Address { get; set; }

        public ICollection<CurrencyRateByTimeServiceModel> CurrencyRateByTime { get; set; }

        public string CityId { get; set; }

        public string BankId { get; set; }
    }
}
