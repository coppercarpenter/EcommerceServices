using Ecommerce.DTO.DbModels;
using Ecommerce.Repositories.Interfaces.Base;

namespace Ecommerce.Repositories.Interfaces
{
    public interface IProductFeatureRepository : IRepositoryBase<ProductFeature>
    {
        ProductFeature GetProductFeature(long id);

        IQueryable<ProductFeature> GetProductFeatures();
    }
}