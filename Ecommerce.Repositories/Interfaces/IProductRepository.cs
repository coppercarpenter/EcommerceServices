using Ecommerce.DTO.DbModels;
using Ecommerce.Repositories.Interfaces.Base;

namespace Ecommerce.Repositories.Interfaces
{
    public interface IProductRepository : IRepositoryBase<Product>
    {
        Product GetProduct(long id);

        bool AnyProduct(long id);

        IQueryable<Product> GetProducts();
    }
}
