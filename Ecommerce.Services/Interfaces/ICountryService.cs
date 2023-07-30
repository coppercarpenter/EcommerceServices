using Ecommerce.DTO.DbModels;

namespace Ecommerce.Services.Interfaces
{
    public interface ICountryService
    {
        Country AddCountry(string name, string image);

        Country EditCountry(long id, string name, string image);

        Country GetCountry(long id);

        IQueryable<Country> GetCountries();

        bool RemoveCountry(long id);
    }
}