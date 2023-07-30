using Ecommerce.Attributes;
using Ecommerce.Common;
using Ecommerce.Common.JWT;
using Ecommerce.Converters;
using Ecommerce.DTO.Models;
using Ecommerce.DTO.Models.Common;
using Ecommerce.Services.Interfaces.Unit;
using Microsoft.AspNetCore.Mvc;

namespace Ecommerce.Controllers
{
    [ApiController]
    [Route("v1/auth")]
    public class AuthController : ControllerBase
    {
        private readonly IServiceUnit _service;
        private readonly Converter _converter;

        public AuthController(IServiceUnit service, Converter converter)
        {
            _service = service;
            _converter = converter;
        }

        [HttpPost("login")]
        public ActionResult<ResponseWrapper<LoginResponse<SellerResponse>>> Login(LoginRequest model)
        {
            var seller = _service.Seller.GetSellers().FirstOrDefault(f => f.Username.Equals(model.Username) && f.Password.Equals(model.Password));
            if (seller == null)
                return Unauthorized(new ResponseWrapper<object>(false, "Invalid username or password", null, null));

            var jwtToken = TokenManger.GenerateToken(seller.Id, AccountType.Seller);

            return Ok(new ResponseWrapper<LoginResponse<SellerResponse>>()
            {
                Data = new LoginResponse<SellerResponse>()
                {
                    Role = AccountType.Seller.ToString(),
                    Token = jwtToken,
                    User = _converter.GetSellerResponse(seller)
                },
                Message = "Successfully Login",
                Success = true,
            });
        }

        [HttpPost("admin_login")]
        public ActionResult<ResponseWrapper<LoginResponse<UserResponse>>> LoginAdmin(LoginRequest model)
        {
            var seller = _service.User.GetUsers().FirstOrDefault(f => f.Username.Equals(model.Username) && f.Password.Equals(model.Password));
            if (seller == null)
                return Unauthorized(new ResponseWrapper<object>(false, "Invalid username or password", null, null));

            var jwtToken = TokenManger.GenerateToken(seller.Id, AccountType.Seller);

            return Ok(new ResponseWrapper<LoginResponse<UserResponse>>()
            {
                Data = new LoginResponse<UserResponse>()
                {
                    Role = AccountType.Admin.ToString(),
                    Token = jwtToken,
                    User = _converter.GetUserResponse(seller)
                },
                Message = "Successfully Login",
                Success = true,
            });
        }

        [CheckJwt]
        [HttpGet("current")]
        public ActionResult<ResponseWrapper<LoginResponse<SellerResponse>>> Current()
        {
            var token = _converter.GetAdminToken(HttpContext);
            var account = _service.Seller.GetSeller(token.Id);

            return Ok(new ResponseWrapper<LoginResponse<SellerResponse>>()
            {
                Data = new LoginResponse<SellerResponse>()
                {
                    Role = token.Type.ToString(),
                    User = _converter.GetSellerResponse(account)
                },
                Message = "Successfully Login",
                Success = true,
            });
        }
    }
}