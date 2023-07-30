using Ecommerce.DTO.DbModels;
using Ecommerce.Repositories.Interfaces.Base;

namespace Ecommerce.Repositories.Interfaces
{
    public interface ICityRepository : IRepositoryBase<City>
    {
        City GetCity(long id);

        bool AnyCity(long id);

        IQueryable<City> GetCities();
    }
}