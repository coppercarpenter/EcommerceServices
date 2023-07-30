using Ecommerce.Common.Exceptions;
using Ecommerce.DTO.DbModels;
using Ecommerce.Repositories.Interfaces.Unit;
using Ecommerce.Services.Interfaces;

namespace Ecommerce.Services.Implementations
{

    internal class CityService : ICityService
    {
        #region Private Fields

        private readonly IRepositoryUnit _repo;

        #endregion Private Fields

        #region Constructors

        public CityService(IRepositoryUnit repo)
        {
            _repo = repo;
        }

        #endregion Constructors

        #region Methods

        public City EditCity(long id, string name, long country_Id)
        {
            if (GetCities().Any(a => a.Name.Equals(name) && a.Country_Id == country_Id && a.Id != id))
                throw new AlreadyExistException("City");

            var city = GetCity(id) ?? throw new NotFoundException("City");

            city.Name = name;

            _repo.City.Update(city);
            _repo.Save();

            return city;
        }

        public IQueryable<City> GetCities()
        {
            return _repo.City.GetCities();
        }

        public City GetCity(long id)
        {
            return _repo.City.GetCity(id);
        }

        public City AddCity(string name, long country_Id)
        {
            if (GetCities().Any(a => a.Name.Equals(name) && a.Country_Id == country_Id))
                throw new AlreadyExistException("City");

            if (_repo.Country.AnyCountry(country_Id))
                throw new NotFoundException("Country");

            var city = new City
            {
                Name = name,
                Country_Id = country_Id
            };

            _repo.City.Create(city);
            _repo.Save();

            return city;
        }

        public bool RemoveCity(long id)
        {
            var city = GetCity(id) ?? throw new NotFoundException("City");

            _repo.City.Delete(city);
            _repo.Save<City>(city);

            return true;
        }

        #endregion Methods
    }
}