using System.Collections.Generic;
using System.Linq;

namespace DataAccess.DataBase.ModelsHelpers
{
    public static class BankHelper
    {
        public static bool EqualsByName(this Bank source, Bank toCompare)
        {
            if (source.Name == null && toCompare.Name == null)
                return false;

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
                    foreach (var secondBankDepartment in secondBank.BankDepartment)
                    {
                        var firstBankDepartment =
                            firstBank.BankDepartment.FindDepartmentInSequence(secondBankDepartment);
                        if (firstBankDepartment == null)
                        {
                            firstBank.BankDepartment.Add(secondBankDepartment);
                        }
                        else
                        {
                            foreach (var rateByTime in secondBankDepartment.CurrencyRateByTime)
                            {
                                firstBankDepartment.CurrencyRateByTime.Add(rateByTime);
                            }
                        }
                    }
                }
            }
        }
    }
}