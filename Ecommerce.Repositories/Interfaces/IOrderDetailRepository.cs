using Ecommerce.DTO.DbModels;
using Ecommerce.Repositories.Interfaces.Base;

namespace Ecommerce.Repositories.Interfaces
{
    public interface IOrderDetailRepository : IRepositoryBase<OrderDetail>
    {
        OrderDetail GetOrderDetail(long id);

        IQueryable<OrderDetail> GetOrderDetails();
    }
}