using Ecommerce.DTO.DbModels;
using Ecommerce.Repositories.Interfaces.Base;

namespace Ecommerce.Repositories.Interfaces
{
    public interface ISellerRepository : IRepositoryBase<Seller>
    {
        Seller GetSeller(long id);

        bool AnySeller(long id);

        IQueryable<Seller> GetSellers();
    }
}