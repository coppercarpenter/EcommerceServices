using Ecommerce.Common.Exceptions;
using Ecommerce.DTO.DbModels;
using Ecommerce.Repositories.Interfaces.Unit;
using Ecommerce.Services.Interfaces;

namespace Ecommerce.Services.Implementations
{
    internal class ProductFeatureService : IProductFeatureService
    {
        #region Private Fields

        private readonly IRepositoryUnit _repo;

        #endregion Private Fields

        #region Constructors

        public ProductFeatureService(IRepositoryUnit repo)
        {
            _repo = repo;
        }

        #endregion Constructors

        #region Methods

        public ProductFeature AddProductFeature(long product_Id, string featureKey, string featureValue)
        {
            if (!_repo.Product.AnyProduct(product_Id))
                throw new NotFoundException("Product");

            var productFeature = new ProductFeature
            {
                FeatureKey = featureKey,
                Product_Id = product_Id,
                FeatureValue = featureValue
            };

            _repo.ProductFeature.Create(productFeature);
            _repo.Save();

            return productFeature;
        }

        public ProductFeature GetProductFeature(long id)
        {
            return _repo.ProductFeature.GetProductFeature(id);
        }

        public IQueryable<ProductFeature> GetProductFeatures()
        {
            return _repo.ProductFeature.GetProductFeatures();
        }

        public bool RemoveProductFeature(long id)
        {
            var productfeature = GetProductFeature(id) ??
                throw new NotFoundException("Product Feature");

            _repo.ProductFeature.Delete(productfeature);
            _repo.Save(productfeature);

            return true;
        }

        #endregion Methods
    }
}