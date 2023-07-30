using Ecommerce.Common.Exceptions;
using Ecommerce.DTO.DbModels;
using Ecommerce.Repositories.Interfaces.Unit;
using Ecommerce.Services.Interfaces;

namespace Ecommerce.Services.Implementations
{
    internal class CustomerService : ICustomerService
    {
        #region Private Fields

        private readonly IRepositoryUnit _repo;

        #endregion Private Fields

        #region Constructors

        public CustomerService(IRepositoryUnit repo)
        {
            _repo = repo;
        }

        #endregion Constructors

        #region Methods

        public Customer EditCustomer(long id, string username, string firstname, string lastname, string email)
        {
            if (GetCustomers().Any(a => a.Username.Equals(username) && a.Id != id))
                throw new AlreadyExistException("Username");

            if (GetCustomers().Any(a => a.Email.Equals(email) && a.Id != id))
                throw new AlreadyExistException("Email");

            var customer = GetCustomer(id) ?? throw new NotFoundException("Customer");

            customer.Username = username;
            customer.Firstname = firstname;
            customer.Lastname = lastname;
            customer.Email = email;

            _repo.Customer.Update(customer);
            _repo.Save();

            return customer;
        }

        public IQueryable<Customer> GetCustomers()
        {
            return _repo.Customer.GetCustomers();
        }

        public Customer GetCustomer(long id)
        {
            return _repo.Customer.GetCustomer(id);
        }

        public Customer AddCustomer(string username, string password, string firstname, string lastname, string email)
        {
            if (GetCustomers().Any(a => a.Username.Equals(username)))
                throw new AlreadyExistException("Username");

            if (GetCustomers().Any(a => a.Email.Equals(email)))
                throw new AlreadyExistException("Email");

            var customer = new Customer
            {
                Email = email,
                Lastname = lastname,
                Firstname = firstname,
                Username = username,
                Password = password
            };

            _repo.Customer.Create(customer);
            _repo.Save();

            return customer;
        }

        public bool RemoveCustomer(long id)
        {
            var customer = GetCustomer(id) ?? throw new NotFoundException("Customer");

            _repo.Customer.Delete(customer);
            _repo.Save<Customer>(customer);

            return true;
        }

        #endregion Methods
    }
}