using Ecommerce.DTO.DbModels;
using Ecommerce.EF;
using Ecommerce.Repositories.Implementations.Base;
using Ecommerce.Repositories.Interfaces;

namespace Ecommerce.Repositories.Implementations
{
    internal class CartProductRepository : RepositoryBase<CartProduct>, ICartProductRepository
    {
        #region Constructors

        public CartProductRepository(EContext db)
            : base(db)
        {
        }

        #endregion Constructors

        #region Methods

        public IQueryable<CartProduct> GetCartProducts()
        {
            return FindAll();
        }

        public CartProduct GetCartProduct(long id)
        {
            return FindAll().FirstOrDefault(a => a.Id == id);
        }

        #endregion Methods
    }
}