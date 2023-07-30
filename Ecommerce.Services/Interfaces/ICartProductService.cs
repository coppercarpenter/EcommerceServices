using Ecommerce.DTO.DbModels;

namespace Ecommerce.Services.Interfaces
{
    public interface ICartProductService
    {
        CartProduct AddProductToCart(long customer_id, long product_id, int qty);

        bool RemoveProductFromCart(long cart_id, long product_id);

        bool RemoveProductFromCart(long id);

        IQueryable<CartProduct> GetCartProducts();
    }
}