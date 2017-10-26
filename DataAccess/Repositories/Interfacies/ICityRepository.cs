using System.Collections.Generic;
using System.Threading.Tasks;
using DataAccess.ModelsForServices;

namespace DataAccess.Repositories.Interfacies
{
    public interface ICityRepository
    {
        Task<ICollection<CityServiceModel>> GetAll();
    }
}
