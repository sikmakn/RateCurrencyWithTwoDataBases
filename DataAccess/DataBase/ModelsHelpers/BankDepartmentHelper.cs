using System.Collections.Generic;
using System.Linq;

namespace DataAccess.DataBase.ModelsHelpers
{
    public static class BankDepartmentHelper
    {
        public static BankDepartment GetNewBankDepartment(string address, string departmentName, int cityId)
        {
            return new BankDepartment
            {
                Address = address,
                Name = departmentName,
                CurrencyRateByTime = new List<CurrencyRateByTime>(),
                CityId = cityId,
            };
        }

        public static BankDepartment FindDepartmentInEnumerable(this IEnumerable<BankDepartment> departments, BankDepartment department)
        {
            return departments.FirstOrDefault(x => x.Name.Contains(department.Name) && x.Address.Contains(department.Address));
        }

        public static bool Equals( this BankDepartment firstDepartment, BankDepartment seconDepartment)
        {
            return firstDepartment.Name.Contains(seconDepartment.Name) &&
                   firstDepartment.Address.Contains(seconDepartment.Address);

        }

        public static void IncludeDepartmentSequence(this ICollection<BankDepartment> source, IEnumerable<BankDepartment> departments)
        {
            foreach (var department in departments)
            {
                var oldDepartment = source.FindDepartmentInEnumerable(department);
                if (oldDepartment == null)
                {
                    source.Add(department);
                }
                else
                {
                    foreach (var rateByTime in department.CurrencyRateByTime)
                    {
                        oldDepartment.CurrencyRateByTime.Add(rateByTime);
                    }
                }
            }
        }
    }
}
