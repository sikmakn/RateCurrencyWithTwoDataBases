

using System.Collections.Generic;
using System.Linq;

namespace DataAccess.DataBase.ModelsHelpers
{
    public static class BankHelper
    {
        public static bool EqualsByName(this Bank source, Bank toCompare)
        {
            return source.Name == toCompare.Name;
        }

        public static void IncludeSequence(this ICollection<Bank> source, IEnumerable<Bank> banks)
        {
            foreach (var secondBank in banks)
            {
                var firstBank = source.FirstOrDefault(x => x.EqualsByName(secondBank));
                if (firstBank == null)
                {
                    source.Add(secondBank);
                }
                else
                {
                    var firstBankDepartment = firstBank.BankDepartment.FindDepartmentInEnumerable(secondBank.BankDepartment.Single());
                    if (firstBankDepartment == null)
                    {
                        firstBank.BankDepartment.Add(secondBank.BankDepartment.Single());
                    }
                    else
                    {
                        foreach (var rateByTime in secondBank.BankDepartment.Single().CurrencyRateByTime)
                        {
                            firstBankDepartment.CurrencyRateByTime.Add(rateByTime);
                        }
                    }
                }
            }
        }
    }
}
