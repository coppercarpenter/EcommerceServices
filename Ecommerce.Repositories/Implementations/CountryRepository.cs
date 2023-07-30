using Ecommerce.DTO.DbModels;
using Ecommerce.EF;
using Ecommerce.Repositories.Implementations.Base;
using Ecommerce.Repositories.Interfaces;

namespace Ecommerce.Repositories.Implementations
{
    internal class CountryRepository : RepositoryBase<Country>, ICountryRepository
    {
        #region Constructors

        public CountryRepository(EContext db)
            : base(db)
        {
        }

        #endregion Constructors

        #region Methods

        public bool AnyCountry(long id)
        {
            return FindAll().Any(a => a.Id == id);
        }

        public IQueryable<Country> GetCountries()
        {
            return FindAll();
        }

        public Country GetCountry(long id)
        {
            return FindAll().FirstOrDefault(a => a.Id == id);
        }

        #endregion Methods
    }
}