using Ecommerce.DTO.DbModels;
using Ecommerce.Repositories.Interfaces.Base;

namespace Ecommerce.Repositories.Interfaces
{
    public interface IProductImageRepository : IRepositoryBase<ProductImage>
    {
        ProductImage GetProductImage(long id);

        IQueryable<ProductImage> GetProductImages();
    }
}
