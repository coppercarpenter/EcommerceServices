using Ecommerce.Common.Exceptions;
using Ecommerce.DTO.DbModels;
using Ecommerce.Repositories.Interfaces.Unit;
using Ecommerce.Services.Interfaces;

namespace Ecommerce.Services.Implementations
{
    internal class CountryService : ICountryService
    {
        #region Private Fields

        private readonly IRepositoryUnit _repo;

        #endregion Private Fields

        #region Constructors

        public CountryService(IRepositoryUnit repo)
        {
            _repo = repo;
        }

        #endregion Constructors

        #region Methods

        public Country EditCountry(long id, string name, string image)
        {
            if (GetCountries().Any(a => a.Name.Equals(name) && a.Id != id))
                throw new AlreadyExistException("Country");

            var country = GetCountry(id) ?? throw new NotFoundException("Country");

            country.Name = name;
            country.Flag = image;

            _repo.Country.Update(country);
            _repo.Save();

            return country;
        }

        public IQueryable<Country> GetCountries()
        {
            return _repo.Country.GetCountries();
        }

        public Country GetCountry(long id)
        {
            return _repo.Country.GetCountry(id);
        }

        public Country AddCountry(string name, string image)
        {
            if (GetCountries().Any(a => a.Name.Equals(name)))
                throw new AlreadyExistException("City");

            var country = new Country
            {
                Name = name,
                Flag = image
            };

            _repo.Country.Create(country);
            _repo.Save();

            return country;
        }

        public bool RemoveCountry(long id)
        {
            var country = GetCountry(id) ?? throw new NotFoundException("City");

            _repo.Country.Delete(country);
            _repo.Save<Country>(country);

            return true;
        }

        #endregion Methods
    }
}