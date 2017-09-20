using System;
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

        public static bool EqualsByNameAndAddress(this BankDepartment firstDepartment, BankDepartment secondDepartment)
        {
            if (secondDepartment == null) throw new NullReferenceException();

            if (secondDepartment.Name == null && secondDepartment.Address == null)
                return firstDepartment.Name == null && firstDepartment.Address == null;

            if (secondDepartment.Name == null)
            {
                return firstDepartment.Name == null && firstDepartment.Address.Contains(secondDepartment.Address);
            }
            if (secondDepartment.Address == null)
            {
                return firstDepartment.Address == secondDepartment.Address && firstDepartment.Name.Contains(secondDepartment.Name);
            }
            return
                (firstDepartment.Name?.Contains(secondDepartment.Name) ?? false) && (firstDepartment.Address?.Contains(secondDepartment.Address) ?? false);
        }


        public static BankDepartment FindDepartmentInSequence(this IEnumerable<BankDepartment> departments, BankDepartment department)
        {
            if(department == null) throw new NullReferenceException();
            return departments.FirstOrDefault(x => x.EqualsByNameAndAddress(department));
        }


        public static void IncludeSequence(this ICollection<BankDepartment> source, IEnumerable<BankDepartment> departments)
        {
            foreach (var department in departments)
            {
                var oldDepartment = source.FindDepartmentInSequence(department);
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
