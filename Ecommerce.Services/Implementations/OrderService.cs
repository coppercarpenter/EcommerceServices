using Ecommerce.Common.Exceptions;
using Ecommerce.DTO.DbModels;
using Ecommerce.Repositories.Interfaces.Unit;
using Ecommerce.Services.Interfaces;

namespace Ecommerce.Services.Implementations
{
    internal class OrderService : IOrderService
    {
        #region Private Fields

        private readonly IRepositoryUnit _repo;

        #endregion Private Fields

        #region Constructors

        public OrderService(IRepositoryUnit repo)
        {
            _repo = repo;
        }

        #endregion Constructors

        #region Methods

        public Order AddOrder(long customer_id, long shipping_address, string shipping_terms)
        {
            if (!_repo.Customer.AnyCustomer(customer_id))
                throw new NotFoundException("Customer");

            var order = new Order()
            {
                ShippingTerms = shipping_terms,
                Customer_Id = customer_id,
                InvoiceNumber = GetOrders().Count().ToString("D6")
            };

            _repo.Order.Create(order);
            _repo.Save();

            return order;
        }

        public IQueryable<Order> GetOrders()
        {
            return _repo.Order.GetOrders();
        }

        public Order GetOrder(long id)
        {
            return _repo.Order.GetOrder(id);
        }

        #endregion Methods
    }
}