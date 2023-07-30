using Ecommerce.DTO.DbModels;

namespace Ecommerce.Services.Interfaces
{
    public interface ICurrencyService
    {
        Currency AddCurrency(string name, string symbol);

        Currency EditCurrency(long id, string name, string symbol);

        Currency GetCurrency(long id);

        IQueryable<Currency> GetCurrencies();

        bool RemoveCurrency(long id);
    }
}