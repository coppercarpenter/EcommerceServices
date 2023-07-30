using Ecommerce.DTO.DbModels;
using Ecommerce.EF;
using Ecommerce.Repositories.Implementations.Base;
using Ecommerce.Repositories.Interfaces;

namespace Ecommerce.Repositories.Implementations
{
    internal class ProductFeatureRepository : RepositoryBase<ProductFeature>, IProductFeatureRepository
    {
        #region Constructors

        public ProductFeatureRepository(EContext db)
            : base(db)
        {
        }

        #endregion Constructors

        #region Methods

        public IQueryable<ProductFeature> GetProductFeatures()
        {
            return FindAll();
        }

        public ProductFeature GetProductFeature(long id)
        {
            return FindAll().FirstOrDefault(a => a.Id == id);
        }

        #endregion Methods
    }
}