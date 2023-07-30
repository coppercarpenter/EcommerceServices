using Ecommerce.DTO.DbModels;
using Ecommerce.EF;
using Ecommerce.Repositories.Implementations.Base;
using Ecommerce.Repositories.Interfaces;

namespace Ecommerce.Repositories.Implementations
{
    internal class OrderDetailRepository : RepositoryBase<OrderDetail>, IOrderDetailRepository
    {
        public OrderDetailRepository(EContext db) :
            base(db)
        {
        }

        public IQueryable<OrderDetail> GetOrderDetails()
        {
            return FindAll();
        }

        public OrderDetail GetOrderDetail(long id)
        {
            return FindAll().FirstOrDefault(a => a.Id == id);
        }
    }
}