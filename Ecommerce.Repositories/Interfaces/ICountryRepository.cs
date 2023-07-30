using Ecommerce.DTO.DbModels;
using Ecommerce.Repositories.Interfaces.Base;

namespace Ecommerce.Repositories.Interfaces
{
    public interface ICountryRepository : IRepositoryBase<Country>
    {
        Country GetCountry(long id);

        bool AnyCountry(long id);

        IQueryable<Country> GetCountries();
    }
}