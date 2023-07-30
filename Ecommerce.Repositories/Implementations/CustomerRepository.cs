using Ecommerce.DTO.DbModels;
using Ecommerce.EF;
using Ecommerce.Repositories.Implementations.Base;
using Ecommerce.Repositories.Interfaces;

namespace Ecommerce.Repositories.Implementations
{
    internal class CustomerRepository : RepositoryBase<Customer>, ICustomerRepository
    {
        #region Constructors

        public CustomerRepository(EContext db)
            : base(db)
        {
        }

        #endregion Constructors

        #region Methods

        public bool AnyCustomer(long id)
        {
            return FindAll().Any(a => a.Id == id);
        }

        public IQueryable<Customer> GetCustomers()
        {
            return FindAll();
        }

        public Customer GetCustomer(long id)
        {
            return FindAll().FirstOrDefault(a => a.Id == id);
        }

        #endregion Methods
    }
}