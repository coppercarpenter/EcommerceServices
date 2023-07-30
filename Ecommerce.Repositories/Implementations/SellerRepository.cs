using Ecommerce.DTO.DbModels;
using Ecommerce.EF;
using Ecommerce.Repositories.Implementations.Base;
using Ecommerce.Repositories.Interfaces;

namespace Ecommerce.Repositories.Implementations
{
    internal class SellerRepository : RepositoryBase<Seller>, ISellerRepository
    {
        #region Constructors

        public SellerRepository(EContext db)
            : base(db)
        {
        }

        #endregion Constructors

        #region Methods

        public bool AnySeller(long id)
        {
            return FindAll().Any(a => a.Id == id);
        }

        public IQueryable<Seller> GetSellers()
        {
            return FindAll();
        }

        public Seller GetSeller(long id)
        {
            return FindAll().FirstOrDefault(a => a.Id == id);
        }

        #endregion Methods
    }
}