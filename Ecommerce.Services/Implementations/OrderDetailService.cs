using Ecommerce.Common.Exceptions;
using Ecommerce.DTO.DbModels;
using Ecommerce.Repositories.Interfaces.Unit;
using Ecommerce.Services.Interfaces;

namespace Ecommerce.Services.Implementations
{
    internal class OrderDetailService : IOrderDetailService
    {
        #region Private Fields

        private readonly IRepositoryUnit _repo;

        #endregion Private Fields

        #region Constructors

        public OrderDetailService(IRepositoryUnit repo)
        {
            _repo = repo;
        }

        #endregion Constructors

        #region Methods

        public bool AddOrderDetail(long order_id, long product_id, int quantity)
        {
            var product = _repo.Product.GetProduct(product_id) ??
                throw new NotFoundException("Product");

            if (!_repo.Order.AnyOrder(order_id))
                throw new NotFoundException("Order");

            var orderDetail = new OrderDetail()
            {
                Order_Id = order_id,
                PerUnitPrice = product.IsFlatPrice ? product.FlatPrice.Value : product.MaxPrice.Value,
                ProductName = product.Title,
                Quantity = quantity,
                UnitOfMeasure_Id = product.Uom_Id
            };


            orderDetail.Price = quantity * orderDetail.PerUnitPrice;
            _repo.OrderDetail.Create(orderDetail);

            _repo.Save();

            return true;
        }

        public IQueryable<OrderDetail> GetOrderDetails()
        {
            return _repo.OrderDetail.GetOrderDetails();
        }

        public OrderDetail GetOrderDetail(long id)
        {
            return _repo.OrderDetail.GetOrderDetail(id);
        }

        #endregion Methods
    }
}