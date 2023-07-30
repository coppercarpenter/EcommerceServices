using Ecommerce.DTO.DbModels;
using Ecommerce.Repositories.Interfaces.Base;

namespace Ecommerce.Repositories.Interfaces
{
    public interface ICustomerRepository : IRepositoryBase<Customer>
    {
        Customer GetCustomer(long id);

        bool AnyCustomer(long id);

        IQueryable<Customer> GetCustomers();
    }
}