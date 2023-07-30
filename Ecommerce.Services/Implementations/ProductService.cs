using Ecommerce.Common.Exceptions;
using Ecommerce.DTO.DbModels;
using Ecommerce.Repositories.Interfaces.Unit;
using Ecommerce.Services.Interfaces;

namespace Ecommerce.Services.Implementations
{
    internal class ProductService : IProductService
    {
        #region Private Fields

        private readonly IRepositoryUnit _repo;

        #endregion Private Fields

        #region Constructors

        public ProductService(IRepositoryUnit repo)
        {
            _repo = repo;
        }

        #endregion Constructors

        #region Methods

        public IQueryable<Product> GetProducts()
        {
            return _repo.Product.GetProducts();
        }

        public Product GetProduct(long id)
        {
            return _repo.Product.GetProduct(id);
        }

        public Product AddProduct(string title, string model, string description, string promoCode, long seller_Id,
                                  long category_Id, bool isFlatPrice, decimal? minPrice, decimal? maxPrice,
                                  decimal? flatPrice, long uom_Id, string keyword, long currency_id, string image)
        {
            if (!_repo.UOM.AnyUOM(uom_Id)) throw new NotFoundException("uom");
            if (!_repo.Seller.AnySeller(seller_Id)) throw new NotFoundException("Seller");
            if (!_repo.Currency.AnyCurrency(currency_id)) throw new NotFoundException("Currency");
            if (!_repo.Category.AnyCategory(category_Id)) throw new NotFoundException("Category");

            var product = new Product
            {
                Model = model,
                Title = title,
                Description = description,
                PromoCode = promoCode,
                IsFlatPrice = isFlatPrice,
                Uom_Id = uom_Id,
                Currency_Id = currency_id,
                Category_Id = category_Id,
                Keyword = keyword,
                FlatPrice = flatPrice,
                MaxPrice = maxPrice,
                MinPrice = minPrice,
                Seller_Id = seller_Id,
                Image = image
            };

            _repo.Product.Create(product);
            _repo.Save();

            return product;
        }

        public Product EditProduct(long id, string title, string model, string description, string promoCode,
                                   long category_Id, bool isFlatPrice, decimal? minPrice, decimal? maxPrice,
                                   decimal? flatPrice, long uom_Id, string keyword, long currency_id, string image)
        {
            if (!_repo.UOM.AnyUOM(uom_Id)) throw new NotFoundException("uom");
            if (!_repo.Category.AnyCategory(category_Id)) throw new NotFoundException("Category");
            if (!_repo.Currency.AnyCurrency(currency_id)) throw new NotFoundException("Currency");

            var product = GetProduct(id) ?? throw new NotFoundException("Product");

            product.Model = model;
            product.Title = title;
            product.Description = description;
            product.PromoCode = promoCode;
            product.Currency_Id = currency_id;
            product.IsFlatPrice = isFlatPrice;
            product.FlatPrice = flatPrice;
            product.MinPrice = minPrice;
            product.MaxPrice = maxPrice;
            product.Uom_Id = uom_Id;
            product.Category_Id = category_Id;
            product.Keyword = keyword;
            product.Image = image;

            _repo.Product.Update(product);
            _repo.Save();

            return product;
        }

        public bool RemoveProduct(long id)
        {
            var product = GetProduct(id) ?? throw new NotFoundException("Product");

            var features = _repo.ProductFeature.GetProductFeatures().Where(w => w.Product_Id == id).ToList();
            foreach (var feature in features)
            {
                new ProductFeatureService(_repo).RemoveProductFeature(feature.Id);
            }

            var images = _repo.ProductImage.GetProductImages().Where(w => w.Product_Id == id).ToList();
            foreach (var image in images)
            {
                new ProductImageService(_repo).RemoveProductImage(image.Id);
            }

            var cartProducts = _repo.CartProduct.GetCartProducts().Where(w => w.Product_Id == id).ToList();
            foreach (var item in cartProducts)
            {
                new CartProductService(_repo).RemoveProductFromCart(item.Id);
            }

            _repo.Product.Delete(product);
            _repo.Save<Product>(product);

            return true;
        }

        #endregion Methods
    }
}