using Ecommerce.Common.Exceptions;
using Ecommerce.DTO.DbModels;
using Ecommerce.Repositories.Interfaces.Unit;
using Ecommerce.Services.Interfaces;

namespace Ecommerce.Services.Implementations
{
    internal class ProductImageService : IProductImageService
    {
        #region Private Fields

        private readonly IRepositoryUnit _repo;

        #endregion Private Fields

        #region Constructors

        public ProductImageService(IRepositoryUnit repo)
        {
            _repo = repo;
        }

        #endregion Constructors

        #region Methods

        public ProductImage AddProductImage(long product_Id, string title, string fileName)
        {
            if (!_repo.Product.AnyProduct(product_Id))
                throw new NotFoundException("Product");

            var image = new ProductImage
            {
                Title = title,
                Product_Id = product_Id,
                FileName = fileName
            };

            _repo.ProductImage.Create(image);
            _repo.Save();

            return image;
        }

        public ProductImage GetProductImage(long id)
        {
            return _repo.ProductImage.GetProductImage(id);
        }

        public IQueryable<ProductImage> GetProductImages()
        {
            return _repo.ProductImage.GetProductImages();
        }

        public bool RemoveProductImage(long id)
        {
            var image = GetProductImage(id) ??
                throw new NotFoundException("Product Feature");

            _repo.ProductImage.Delete(image);
            _repo.Save(image);

            return true;
        }

        #endregion Methods

    }
}