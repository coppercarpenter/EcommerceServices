using Ecommerce.DTO.DbModels;

namespace Ecommerce.Services.Interfaces
{
    public interface ICustomerService
    {
        Customer AddCustomer(string username, string password, string firstname, string lastname, string email);

        Customer EditCustomer(long id, string username, string firstname, string lastname, string email);

        Customer GetCustomer(long id);

        IQueryable<Customer> GetCustomers();

        bool RemoveCustomer(long id);
    }
}