using Ecommerce.DTO.DbModels;
using Ecommerce.Repositories.Interfaces.Base;

namespace Ecommerce.Repositories.Interfaces
{
    public interface ICurrencyRepository : IRepositoryBase<Currency>
    {
        Currency GetCurrency(long id);

        bool AnyCurrency(long id);

        IQueryable<Currency> GetCurrencies();
    }
}
