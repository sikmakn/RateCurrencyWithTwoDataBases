using System.Collections.Generic;

namespace DataAccess.ModelsForServices
{
    public class BankServiceModel
    {
        public string Id { get; set; }

        public string Name { get; set; }

        public ICollection<BankDepartmentServiceModel> BankDepartment { get; set; }
    }
}
