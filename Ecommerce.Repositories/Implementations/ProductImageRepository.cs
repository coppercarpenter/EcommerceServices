using Ecommerce.DTO.DbModels;
using Ecommerce.EF;
using Ecommerce.Repositories.Implementations.Base;
using Ecommerce.Repositories.Interfaces;

namespace Ecommerce.Repositories.Implementations
{
    internal class ProductImageRepository : RepositoryBase<ProductImage>, IProductImageRepository
    {
        #region Constructors

        public ProductImageRepository(EContext db)
            : base(db)
        {
        }

        #endregion Constructors

        #region Methods


        public IQueryable<ProductImage> GetProductImages()
        {
            return FindAll();
        }

        public ProductImage GetProductImage(long id)
        {
            return FindAll().FirstOrDefault(a => a.Id == id);
        }

        #endregion Methods
    }
}