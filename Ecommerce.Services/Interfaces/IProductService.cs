using Ecommerce.DTO.DbModels;

namespace Ecommerce.Services.Interfaces
{
    public interface IProductService
    {
        Product AddProduct(string title, string model, string description, string promoCode, long seller_Id,
                           long category_Id, bool isFlatPrice, decimal? minPrice, decimal? maxPrice, decimal? flatPrice,
                           long uom_Id, string keyword, long currency_id, string image);

        Product EditProduct(long id, string title, string model, string description, string promoCode, long category_Id,
                            bool isFlatPrice, decimal? minPrice, decimal? maxPrice, decimal? flatPrice, long uom_Id,
                            string keyword, long currency_id, string image);

        IQueryable<Product> GetProducts();

        Product GetProduct(long id);

        bool RemoveProduct(long id);
    }
}