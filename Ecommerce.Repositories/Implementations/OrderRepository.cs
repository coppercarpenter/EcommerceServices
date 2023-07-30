using Ecommerce.DTO.DbModels;
using Ecommerce.EF;
using Ecommerce.Repositories.Implementations.Base;
using Ecommerce.Repositories.Interfaces;

namespace Ecommerce.Repositories.Implementations
{
    internal class OrderRepository : RepositoryBase<Order>, IOrderRepository
    {
        public OrderRepository(EContext db) :
            base(db)
        {
        }

        public bool AnyOrder(long id)
        {
            return FindAll().Any(a => a.Id == id);
        }

        public IQueryable<Order> GetOrders()
        {
            return FindAll();
        }

        public Order GetOrder(long id)
        {
            return FindAll().FirstOrDefault(a => a.Id == id);
        }
    }
}