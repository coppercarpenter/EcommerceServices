using Ecommerce.DTO.DbModels;

namespace Ecommerce.Services.Interfaces
{
    public interface IOrderDetailService
    {
        bool AddOrderDetail(long order_id, long product_id, int quantity);

        OrderDetail GetOrderDetail(long id);

        IQueryable<OrderDetail> GetOrderDetails();
    }
}