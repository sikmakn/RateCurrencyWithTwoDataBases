using System.Collections.Generic;

namespace DataAccess.ModelsForServices
{
    public class BankDepartmentServiceModel
    {
        public string Id { get; set; }

        public string Name { get; set; }

        public string Address { get; set; }

        public ICollection<CurrencyRateByTimeServiceModel> CurrencyRateByTime { get; set; }

        public CityServiceModel City { get; set; }

        public string BankName { get; set; }
    }
}
