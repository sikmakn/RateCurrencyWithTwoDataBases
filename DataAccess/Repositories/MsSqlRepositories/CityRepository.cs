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
    public class CityRepository: ICityRepository
    {
        private readonly DbSet<City> _dbSet;

        public CityRepository(IUnitOfWork unitOfWork)
        {
            _dbSet = unitOfWork.Context.Set<City>();
        }

        public async Task<ICollection<CityServiceModel>> GetAll()
        {
            var cityServiceModelList = new List<CityServiceModel>();
            await _dbSet.ForEachAsync(x => cityServiceModelList.Add(Mapper.Map<City, CityServiceModel>(x)));
            return cityServiceModelList;
        }
    }
}
