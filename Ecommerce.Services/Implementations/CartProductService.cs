using Ecommerce.Common.Exceptions;
using Ecommerce.DTO.DbModels;
using Ecommerce.Repositories.Interfaces.Unit;
using Ecommerce.Services.Interfaces;

namespace Ecommerce.Services.Implementations
{
    internal class CartProductService : ICartProductService
    {
        #region Private Fields

        private readonly IRepositoryUnit _repo;

        #endregion Private Fields

        #region Constructors

        public CartProductService(IRepositoryUnit repo)
        {
            _repo = repo;
        }

        #endregion Constructors

        #region Methods

        public CartProduct AddProductToCart(long customer_id, long product_id, int qty)
        {

            if (!_repo.Product.AnyProduct(product_id))
                throw new NotFoundException("Product");

            if (!_repo.Customer.AnyCustomer(customer_id))
                throw new NotFoundException("Customer");

            var productCart = GetCartProducts().FirstOrDefault(f => f.Customer_Id == customer_id && f.Product_Id == product_id);
            if (productCart == null)
            {
                productCart = new CartProduct()
                {
                    Customer_Id = customer_id,
                    Product_Id = product_id,
                    Quantity = qty,
                    AddedOn = DateTime.UtcNow
                };
                _repo.CartProduct.Create(productCart);
            }
            else
            {
                productCart.Quantity += qty;

                _repo.CartProduct.Update(productCart);
            }
            _repo.Save();

            return productCart;
        }

        public IQueryable<CartProduct> GetCartProducts()
        {
            return _repo.CartProduct.GetCartProducts();
        }

        public CartProduct GetCartProduct(long id)
        {
            return _repo.CartProduct.GetCartProduct(id);
        }
        public bool RemoveProductFromCart(long customer_id, long product_id)
        {
            var cartProduct = GetCartProducts().FirstOrDefault(f => f.Customer_Id == customer_id && f.Product_Id == product_id) ??
                throw new NotFoundException("Product in cart");

            _repo.CartProduct.Delete(cartProduct);
            _repo.Save<CartProduct>(cartProduct);

            return true;
        }

        public bool RemoveProductFromCart(long id)
        {
            var cartProduct = GetCartProduct(id)??
                throw new NotFoundException("Product in cart");

            _repo.CartProduct.Delete(cartProduct);
            _repo.Save<CartProduct>(cartProduct);

            return true;
        }

        #endregion Methods
    }
}