using Ecommerce.DTO.DbModels;
using Ecommerce.EF;
using Ecommerce.Repositories.Implementations.Base;
using Ecommerce.Repositories.Interfaces;

namespace Ecommerce.Repositories.Implementations
{
    internal class CityRepository : RepositoryBase<City>, ICityRepository
    {
        #region Constructors

        public CityRepository(EContext db)
            : base(db)
        {
        }

        #endregion Constructors

        public bool AnyCity(long id)
        {
            return FindAll().Any(a => a.Id == id);
        }

        public IQueryable<City> GetCities()
        {
            return FindAll();
        }

        public City GetCity(long id)
        {
            return FindAll().FirstOrDefault(a => a.Id == id);
        }
    }
}