using Ecommerce.DTO.DbModels;

namespace Ecommerce.Services.Interfaces
{
    public interface ICityService
    {

        City AddCity(string name,long country_Id);

        City EditCity(long id, string name, long country_Id);

        City GetCity(long id);

        IQueryable<City> GetCities();

        bool RemoveCity(long id);
    }
}