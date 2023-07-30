namespace Ecommerce.Repositories.Interfaces.Unit
{
    public interface IRepositoryUnit
    {
        void BeginTransaction();

        void CommitTransaction();

        void RollBackTransaction();

        void Save();

        void Save<TEntity>(TEntity entity);

        ICartProductRepository CartProduct { get; }
        ICategoryRepository Category { get; }
        ICityRepository City { get; }
        ICountryRepository Country { get; }
        ICurrencyRepository Currency { get; }
        ICustomerRepository Customer { get; }
        IOrderDetailRepository OrderDetail { get; }
        IOrderRepository Order { get; }
        IProductFeatureRepository ProductFeature { get; }
        IProductImageRepository ProductImage { get; }
        IProductRepository Product { get; }
        ISellerRepository Seller { get; }
        IUOMRepository UOM { get; }
        IUserRepository User { get; }
    }
}