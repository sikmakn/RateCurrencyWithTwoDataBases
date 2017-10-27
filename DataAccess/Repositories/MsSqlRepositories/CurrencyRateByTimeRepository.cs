using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using DataAccess.DataBase;
using DataAccess.ModelsForServices;
using DataAccess.Repositories.Interfacies;
using DataAccess.UnitOfWork;

namespace DataAccess.Repositories.MsSqlRepositories
{
    public class CurrencyRateByTimeRepository: ICurrencyRateByTimeRepository
    {
        private readonly DbSet<CurrencyRateByTime> _currencyRateByTimes;

        public CurrencyRateByTimeRepository(IUnitOfWork unitOfWork)
        {
            _currencyRateByTimes = unitOfWork.Context.Set<CurrencyRateByTime>();
        }

        public async Task<IQueryable<CurrencyRateByTimeServiceModel>> GetAll()
        {
            var currencyServiceModelList = new List<CurrencyRateByTimeServiceModel>();
            await _currencyRateByTimes.ForEachAsync(x => currencyServiceModelList.Add(Convert(x)));
            return currencyServiceModelList.AsQueryable();
        }
        public async Task<CurrencyRateByTimeServiceModel> GetById(string id)
        {
            var flag = int.TryParse(id, out var idInt);

            if (!flag)
                return null;

            var currencyRate =  await _currencyRateByTimes.FindAsync(idInt);
            return Convert(currencyRate);
        }

        private static CurrencyRateByTimeServiceModel Convert(CurrencyRateByTime currencyRate)
        {
            return new CurrencyRateByTimeServiceModel
            {
                Id = currencyRate.Id.ToString(),
                DateTime = currencyRate.DateTime,
                Sale = currencyRate.Sale,
                Purchase = currencyRate.Purchase,
                BankDepartment = new BankDepartmentServiceModel
                {
                    Id = currencyRate.BankDepartment.Id.ToString(),
                    Address = currencyRate.BankDepartment.Address,
                    Name = currencyRate.BankDepartment.Name,
                    BankId = currencyRate.BankDepartment.BankId.ToString(),
                    CityId = currencyRate.BankDepartment.CityId.ToString()
                },
                BankDepartmentId = currencyRate.BankDepartment.Id.ToString(),
                CurrencyId = currencyRate.CurrencyId.ToString()
            };
        }

        public async Task<IQueryable<CurrencyRateByTimeServiceModel>> GetAllActuall()
        {
            var span = new TimeSpan(4, 0, 0).TotalHours;
            var now = DateTime.UtcNow;
            var currencyResult = _currencyRateByTimes.Where(x =>
                DbFunctions.DiffMonths(now, x.DateTime) == 0
                && DbFunctions.DiffYears(now, x.DateTime) == 0
                && DbFunctions.DiffDays(now, x.DateTime) == 0
                & DbFunctions.DiffHours(x.DateTime, now) < span);
            var currencyRateServiceModel = new List<CurrencyRateByTimeServiceModel>();
            await currencyResult.ForEachAsync(x => currencyRateServiceModel.Add(new CurrencyRateByTimeServiceModel
            {
                Id = x.Id.ToString(),
                DateTime = x.DateTime,
                Purchase = x.Purchase,
                Sale = x.Sale,
                CurrencyId = x.CurrencyId.ToString(),
                BankDepartmentId = x.BankDepartment.Id.ToString(),
                BankDepartment = new BankDepartmentServiceModel
                {
                    Id = x.BankDepartment.Id.ToString(),
                    Address = x.BankDepartment.Address,
                    Name = x.BankDepartment.Name,
                    BankId = x.BankDepartment.BankId.ToString(),
                    CityId = x.BankDepartment.CityId.ToString()
                }
            }));
            return currencyRateServiceModel.AsQueryable();
        }
    }
}
