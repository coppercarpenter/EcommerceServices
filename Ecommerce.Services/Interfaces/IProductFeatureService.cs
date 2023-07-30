using Ecommerce.DTO.DbModels;

namespace Ecommerce.Services.Interfaces
{
    public interface IProductFeatureService
    {
        ProductFeature AddProductFeature(long product_Id, string featureKey, string featureValue);

        bool RemoveProductFeature(long id);

        IQueryable<ProductFeature> GetProductFeatures();
    }
}