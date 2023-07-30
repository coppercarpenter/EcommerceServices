using Ecommerce.DTO.DbModels;

namespace Ecommerce.Services.Interfaces
{
    public interface IOrderService
    {
        Order AddOrder(long customer_id, long shipping_address, string shipping_terms);

        Order GetOrder(long id);

        IQueryable<Order> GetOrders();
    }
}