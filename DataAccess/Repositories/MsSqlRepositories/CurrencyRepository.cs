using System.Collections.Generic;
using System.Data.Entity;
using System.Threading.Tasks;
using AutoMapper;
using DataAccess.DataBase;
using DataAccess.ModelsForServices;
using DataAccess.Repositories.Interfacies;
using DataAccess.UnitOfWork;

namespace DataAccess.Repositories.MsSqlRepositories
{
    public class CurrencyRepository: ICurrencyRepository
    {
        private readonly DbSet<Currency> _dbSet;

        public CurrencyRepository(IUnitOfWork unitOfWork)
        {
            _dbSet = unitOfWork.Context.Set<Currency>();
        }

        public async Task<ICollection<CurrencyServiceModel>> GetAll()
        {
            var currencyServiceModelList = new List<CurrencyServiceModel>();
            await _dbSet.ForEachAsync(x => currencyServiceModelList.Add(Mapper.Map<Currency, CurrencyServiceModel>(x)));
            return currencyServiceModelList;
        }
    }
}
