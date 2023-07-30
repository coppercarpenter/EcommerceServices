namespace Ecommerce.Services.Interfaces.Unit
{
    public interface IServiceUnit
    {
        void BeginTransaction();

        void CommitTransaction();

        void RollbackTransaction();

        ICartProductService CartProduct { get; }
        ICategoryService Category { get; }
        ICityService City { get; }
        ICountryService Country { get; }
        ICurrencyService Currency { get; }
        ICustomerService Customer { get; }
        IOrderService Order { get; }
        IOrderDetailService OrderDetail { get; }
        IProductFeatureService ProductFeature { get; }
        IProductImageService ProductImage { get; }
        IProductService Product { get; }
        ISellerService Seller { get; }
        IUOMService UOM { get; }
        IUserService User { get; }
    }
}