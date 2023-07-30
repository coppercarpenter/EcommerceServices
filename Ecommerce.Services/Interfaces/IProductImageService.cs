using Ecommerce.DTO.DbModels;

namespace Ecommerce.Services.Interfaces
{
    public interface IProductImageService
    {
        ProductImage AddProductImage(long product_Id, string title, string fileName);

        bool RemoveProductImage(long id);

        IQueryable<ProductImage> GetProductImages();
    }
}