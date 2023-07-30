using Ecommerce.Common.Exceptions;
using Ecommerce.DTO.DbModels;
using Ecommerce.Repositories.Interfaces.Unit;
using Ecommerce.Services.Interfaces;

namespace Ecommerce.Services.Implementations
{
    internal class CurrencyService : ICurrencyService
    {
        #region Private Fields

        private readonly IRepositoryUnit _repo;

        #endregion Private Fields

        #region Constructors

        public CurrencyService(IRepositoryUnit repo)
        {
            _repo = repo;
        }

        #endregion Constructors

        #region Methods

        public IQueryable<Currency> GetCurrencies()
        {
            return _repo.Currency.GetCurrencies();
        }

        public Currency GetCurrency(long id)
        {
            return _repo.Currency.GetCurrency(id);
        }

        public Currency AddCurrency(string name, string symbol)
        {
            if (GetCurrencies().Any(a => a.Name.Equals(name))) throw new AlreadyExistException("Currency");

            var currency = new Currency
            {
                Name = name,
                CurrencySymbol = symbol
            };

            _repo.Currency.Create(currency);
            _repo.Save();

            return currency;
        }

        public Currency EditCurrency(long id, string name, string symbol)
        {
            var currency = GetCurrency(id) ?? throw new NotFoundException("currency");

            currency.Name = name;
            currency.CurrencySymbol = symbol;

            _repo.Currency.Update(currency);
            _repo.Save();

            return currency;
        }

        public bool RemoveCurrency(long id)
        {
            var currency = GetCurrency(id) ?? throw new NotFoundException("currency");

            _repo.Currency.Delete(currency);
            _repo.Save();

            return true;
        }

        #endregion Methods
    }
}