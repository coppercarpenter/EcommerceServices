using Ecommerce.DTO.DbModels;
using Ecommerce.EF;
using Ecommerce.Repositories.Implementations.Base;
using Ecommerce.Repositories.Interfaces;

namespace Ecommerce.Repositories.Implementations
{
    internal class ProductRepository : RepositoryBase<Product>, IProductRepository
    {
        #region Constructors

        public ProductRepository(EContext db)
            : base(db)
        {
        }

        #endregion Constructors

        #region Methods

        public bool AnyProduct(long id)
        {
            return FindAll().Any(a => a.Id == id);
        }

        public IQueryable<Product> GetProducts()
        {
            return FindAll();
        }

        public Product GetProduct(long id)
        {
            return FindAll().FirstOrDefault(a => a.Id == id);
        }

        #endregion Methods
    }
}