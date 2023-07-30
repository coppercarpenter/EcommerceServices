using Ecommerce.Repositories.Interfaces.Unit;
using Ecommerce.Services.Interfaces;
using Ecommerce.Services.Interfaces.Unit;

namespace Ecommerce.Services.Implementations.Unit
{
    internal class ServiceUnit : IServiceUnit
    {
        #region Private Fields

        private readonly IRepositoryUnit _repo;

        private ICartProductService _cartProduct;
        private ICategoryService _category;
        private ICityService _city;
        private ICustomerService _customer;
        private ICountryService _country;
        private ICurrencyService _currency;
        private IOrderDetailService _orderDetail;
        private IOrderService _order;
        private IProductFeatureService _productFeature;
        private IProductImageService _productImage;
        private IProductService _product;
        private ISellerService _seller;
        private IUOMService _uom;
        private IUserService _user;

        #endregion Private Fields

        #region Constructors

        public ServiceUnit(IRepositoryUnit repo)
        {
            _repo = repo;
        }

        #endregion Constructors

        #region Properties

        public ICartProductService CartProduct => _cartProduct ??= new CartProductService(_repo);
        public ICategoryService Category => _category ??= new CategoryService(_repo);
        public ICityService City => _city ??= new CityService(_repo);
        public ICustomerService Customer => _customer ??= new CustomerService(_repo);
        public ICountryService Country => _country ??= new CountryService(_repo);
        public ICurrencyService Currency => _currency ??= new CurrencyService(_repo);
        public IOrderService Order => _order ??= new OrderService(_repo);
        public IOrderDetailService OrderDetail => _orderDetail ??= new OrderDetailService(_repo);
        public IProductFeatureService ProductFeature => _productFeature ??= new ProductFeatureService(_repo);
        public IProductImageService ProductImage=> _productImage??= new ProductImageService(_repo);
        public IProductService Product => _product ??= new ProductService(_repo);
        public ISellerService Seller => _seller ??= new SellerService(_repo);
        public IUOMService UOM => _uom ??= new UOMService(_repo);
        public IUserService User => _user ??= new UserService(_repo);

        #endregion Properties

        #region Methods

        public void BeginTransaction()
        {
            _repo.BeginTransaction();
        }

        public void CommitTransaction()
        {
            _repo.CommitTransaction();
        }

        public void RollbackTransaction()
        {
            _repo.RollBackTransaction();
        }

        #endregion Methods
    }
}