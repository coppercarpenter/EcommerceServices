using Ecommerce.DTO.DbModels;
using Ecommerce.Repositories.Interfaces.Base;

namespace Ecommerce.Repositories.Interfaces
{
    public interface ICartProductRepository : IRepositoryBase<CartProduct>
    {
        CartProduct GetCartProduct(long id);

        IQueryable<CartProduct> GetCartProducts();
    }
}