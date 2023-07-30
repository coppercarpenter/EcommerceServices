using Ecommerce.DTO.DbModels;
using Ecommerce.EF;
using Ecommerce.Repositories.Implementations.Base;
using Ecommerce.Repositories.Interfaces;

namespace Ecommerce.Repositories.Implementations
{
    internal class CurrencyRepository : RepositoryBase<Currency>, ICurrencyRepository
    {
        #region Constructors

        public CurrencyRepository(EContext db)
            : base(db)
        {
        }

        #endregion Constructors



        #region Methods

        public bool AnyCurrency(long id)
        {
            return FindAll().Any(a => a.Id == id);
        }

        public IQueryable<Currency> GetCurrencies()
        {
            return FindAll();
        }

        public Currency GetCurrency(long id)
        {
            return FindAll().FirstOrDefault(a => a.Id == id);
        }

        #endregion Methods
    }
}