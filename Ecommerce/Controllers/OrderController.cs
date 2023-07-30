using Ecommerce.Attributes;
using Ecommerce.Common;
using Ecommerce.Common.Exceptions;
using Ecommerce.Common.KeysAndValues;
using Ecommerce.Converters;
using Ecommerce.DTO.Models;
using Ecommerce.DTO.Models.Common;
using Ecommerce.Services.Interfaces.Unit;
using Microsoft.AspNetCore.Mvc;

namespace Ecommerce.Controllers
{
    [ApiController]
    [Route("v1/order")]
    public class OrderController : ControllerBase
    {
        #region Private Fields

        private readonly IServiceUnit _service;
        private readonly Converter _converter;

        #endregion Private Fields

        #region Constructors

        public OrderController(IServiceUnit service, Converter converter)
        {
            _service = service;
            _converter = converter;
        }

        #endregion Constructors

        #region Methods

        #region End Points

        #region POST

        [HttpPost("add_to_cart")]
        [CheckJwt(Allows = new AccountType[] { AccountType.Customer })]
        public ActionResult<ResponseWrapper<bool>> AddToCart(AddToCartModel model)
        {
            var token = _converter.GetAdminToken(HttpContext);
            var admin = _service.Customer.GetCustomer(token.Id);

            _service.CartProduct.AddProductToCart(admin.Id, model.Product_Id, model.Quantity);
            return Ok(new ResponseWrapper<bool>
            {
                Data = true,
                Message = MessageHelper.SuccessfullyAdded,
                Success = true
            });
        }

        [HttpPost("create_order")]
        [CheckJwt(Allows = new AccountType[] { AccountType.Customer })]
        public ActionResult<ResponseWrapper<bool>> CreateOrder(AddOrderRequest model)
        {
            var token = _converter.GetAdminToken(HttpContext);
            var admin = _service.Customer.GetCustomer(token.Id);

            _service.BeginTransaction();

            var order = _service.Order.AddOrder(admin.Id, model.ShippingAddress, model.ShippingTerms);

            var collection = _service.CartProduct.GetCartProducts().Where(w => w.Customer_Id == admin.Id);
            foreach (var item in collection)
            {
                _service.CartProduct.RemoveProductFromCart(admin.Id, item.Product_Id);
            }
            foreach (var item in model.List)
            {
                _service.OrderDetail.AddOrderDetail(order.Id, item.Product_Id, item.Quantity);
            }

            _service.CommitTransaction();
            return Ok(new ResponseWrapper<bool>
            {
                Data = true,
                Message = MessageHelper.SuccessfullyAdded,
                Success = true
            });
        }

        #endregion POST

        #region GET

        [HttpGet("get_cart_items")]
        [CheckJwt(Allows = new AccountType[] { AccountType.Customer })]
        public ActionResult<ResponseWrapper<List<CartResponse>>> GetCartItems()
        {
            var token = _converter.GetAdminToken(HttpContext);
            var admin = _service.Customer.GetCustomer(token.Id);

            var cartProducts = _service.CartProduct.GetCartProducts().Where(w => w.Customer_Id == admin.Id).ToList();

            var res = new ResponseWrapper<List<CartResponse>>()
            {
                Data = new List<CartResponse>(),
                Success = true,
                Message = MessageHelper.SuccessfullyGet
            };
            foreach (var product in cartProducts)
            {
                res.Data.Add(_converter.GetCartResponse(product));
            }
            return Ok(res);
        }

        [HttpGet("")]
        [CheckJwt(Allows = new AccountType[] { AccountType.Customer })]
        public ActionResult<PagedResponse<List<OrderResponse>>> GetOrders(int? pageSize, int? pageIndex)
        {
            var token = _converter.GetAdminToken(HttpContext);
            var admin = _service.Customer.GetCustomer(token.Id);

            var orders = _service.Order.GetOrders().Where(w => w.Customer_Id == admin.Id);

            var total = 0;
            if (pageIndex.HasValue && pageSize.HasValue && pageSize.Value > 0)
            {
                total = (int)Math.Ceiling(orders.Count() / (double)pageSize.Value);
                orders = orders.Skip(pageIndex.Value * pageSize.Value).Take(pageSize.Value);
            }

            var res = new PagedResponse<List<OrderResponse>>()
            {
                Data = new List<OrderResponse>(),
                Success = true,
                Message = MessageHelper.SuccessfullyGet,
                TotalPages = total
            };
            foreach (var order in orders)
            {
                res.Data.Add(_converter.GetOrderResponse(order));
            }
            return Ok(res);
        }

        [HttpGet("{id}")]
        [CheckJwt(Allows = new AccountType[] { AccountType.Customer })]
        public ActionResult<ResponseWrapper<OrderResponse>> GetOrderById(long id)
        {
            var order = _service.Order.GetOrder(id) ?? throw new NotFoundException("Order");
            return Ok(new ResponseWrapper<OrderResponse>()
            {
                Data = _converter.GetOrderResponse(order),
                Success = true,
                Message = MessageHelper.SuccessfullyGet
            });
        }

        [HttpGet("admin")]
        [CheckJwt(Allows = new AccountType[] { AccountType.Admin })]
        public ActionResult<PagedResponse<List<OrderResponse>>> GetOrderAdmin(long? buyer_id, int? pageSize, int? pageIndex)
        {
            var orders = _service.Order.GetOrders();

            if (buyer_id.HasValue)
                orders = orders.Where(w => w.Customer_Id == buyer_id);

            var total = 0;
            if (pageIndex.HasValue && pageSize.HasValue && pageSize.Value > 0)
            {
                total = (int)Math.Ceiling(orders.Count() / (double)pageSize.Value);
                orders = orders.Skip(pageIndex.Value * pageSize.Value).Take(pageSize.Value);
            }

            var res = new PagedResponse<List<OrderResponse>>()
            {
                Data = new List<OrderResponse>(),
                Success = true,
                Message = MessageHelper.SuccessfullyGet,
                TotalPages = total
            };
            foreach (var order in orders)
            {
                res.Data.Add(_converter.GetOrderResponse(order));
            }
            return Ok(res);
        }

        #endregion GET

        [HttpPut("remove_from_cart/{product_id}")]
        [CheckJwt(Allows = new AccountType[] { AccountType.Customer })]
        public ActionResult<ResponseWrapper<bool>> RemoveCartQuantity(long product_id)
        {
            var token = _converter.GetAdminToken(HttpContext);
            var admin = _service.Customer.GetCustomer(token.Id);

            return Ok(new ResponseWrapper<bool>
            {
                Data = _service.CartProduct.RemoveProductFromCart(admin.Id, product_id),
                Message = MessageHelper.SuccessfullyUpdated,
                Success = true
            });
        }

        #endregion End Points

        #endregion Methods
    }
}