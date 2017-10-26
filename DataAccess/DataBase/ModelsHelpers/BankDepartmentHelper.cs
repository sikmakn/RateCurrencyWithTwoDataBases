using System;
using System.Collections.Generic;
using System.Linq;
using DataAccess.ModelsForServices;

namespace DataAccess.DataBase.ModelsHelpers
{
    public static class BankDepartmentHelper
    {
        public static BankDepartmentServiceModel GetNewBankDepartment(string address, string departmentName, string cityId)
        {
            return new BankDepartmentServiceModel
            {
                Address = address,
                Name = departmentName,
                CurrencyRateByTime = new List<CurrencyRateByTimeServiceModel>(),
                CityId = cityId,
            };
        }

        public static bool EqualsByNameAndAddress(this BankDepartmentServiceModel firstDepartment, BankDepartmentServiceModel secondDepartment)
        {
            if (secondDepartment == null) throw new NullReferenceException();

            if (secondDepartment.Name == null && secondDepartment.Address == null)
                return firstDepartment.Name == null && firstDepartment.Address == null;

            if (secondDepartment.Name == null)
                return firstDepartment.Name == null && firstDepartment.Address.Contains(secondDepartment.Address);
            
            if (secondDepartment.Address == null)
                return firstDepartment.Address == secondDepartment.Address && firstDepartment.Name.Contains(secondDepartment.Name);

            return
                (firstDepartment.Name?.Contains(secondDepartment.Name) ?? false)
                && (firstDepartment.Address?.Contains(secondDepartment.Address) ?? false);
        }


        public static BankDepartmentServiceModel FindDepartmentInSequence(this IEnumerable<BankDepartmentServiceModel> departments, BankDepartmentServiceModel department)
        {
            if(department == null) throw new NullReferenceException();
            return departments.FirstOrDefault(x => x.EqualsByNameAndAddress(department));
        }


        public static void IncludeSequence(this ICollection<BankDepartmentServiceModel> source, IEnumerable<BankDepartmentServiceModel> departments)
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
