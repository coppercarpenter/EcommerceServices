using Ecommerce.DTO.DbModels;
using Ecommerce.Repositories.Interfaces.Base;

namespace Ecommerce.Repositories.Interfaces
{
    public interface IOrderRepository : IRepositoryBase<Order>
    {
        Order GetOrder(long id);

        bool AnyOrder(long id);

        IQueryable<Order> GetOrders();
    }
}