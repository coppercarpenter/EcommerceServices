using Ecommerce.EF;
using Ecommerce.Repositories.Interfaces;
using Ecommerce.Repositories.Interfaces.Unit;
using Microsoft.EntityFrameworkCore;

namespace Ecommerce.Repositories.Implementations.Unit
{
    internal class RepositoryUnit : IRepositoryUnit
    {
        #region Private Fields

        private readonly EContext _db;

        private ICartProductRepository _cartProduct;
        private ICategoryRepository _category;
        private ICityRepository _city;
        private ICountryRepository _country;
        private ICurrencyRepository _currency;
        private ICustomerRepository _customer;
        private IOrderDetailRepository _orderDetail;
        private IOrderRepository _order;
        private IProductFeatureRepository _productFeature;
        private IProductImageRepository _productImage;
        private IProductRepository _product;
        private ISellerRepository _seller;
        private IUOMRepository _uom;
        private IUserRepository _user;

        #endregion Private Fields

        #region Constructors

        public RepositoryUnit(EContext db)
        {
            _db = db;
        }

        #endregion Constructors

        #region Properties

        public ICartProductRepository CartProduct => _cartProduct ??= new CartProductRepository(_db);
        public ICategoryRepository Category => _category ??= new CategoryRepository(_db);
        public ICityRepository City => _city ??= new CityRepository(_db);
        public ICountryRepository Country => _country ??= new CountryRepository(_db);
        public ICurrencyRepository Currency => _currency ??= new CurrencyRepository(_db);
        public ICustomerRepository Customer => _customer ??= new CustomerRepository(_db);
        public IOrderDetailRepository OrderDetail => _orderDetail ??= new OrderDetailRepository(_db);
        public IOrderRepository Order => _order ??= new OrderRepository(_db);
        public IProductFeatureRepository ProductFeature => _productFeature ??= new ProductFeatureRepository(_db);
        public IProductImageRepository ProductImage => _productImage ??= new ProductImageRepository(_db);
        public IProductRepository Product => _product ??= new ProductRepository(_db);
        public ISellerRepository Seller => _seller ??= new SellerRepository(_db);
        public IUOMRepository UOM => _uom ??= new UOMRepository(_db);
        public IUserRepository User => _user ??= new UserRepository(_db);

        #endregion Properties

        #region Methods

        public void BeginTransaction()
        {
            _db.Database.BeginTransaction();
        }

        public void CommitTransaction()
        {
            _db.Database.CommitTransaction();
        }

        public void RollBackTransaction()
        {
            _db.Database.RollbackTransaction();
        }

        public void Save()
        {
            _db.SaveChanges();
        }

        public void Save<TEntity>(TEntity entity)
        {
            _db.SaveChanges();

            _db.Entry(entity).State = EntityState.Detached;
        }

        #endregion Methods
    }
}