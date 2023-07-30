using Ecommerce.DTO.DbModels;

namespace LalaajiBTB.Services.Interfaces
{
    public interface IProductImageService
    {
        ProductImage AddProductImage(string title, string fileName, long productId);

        ProductImage EditProductImage(string title, string fileName, long productImageId);

        bool RemoveProductImage(long id);
    }
}