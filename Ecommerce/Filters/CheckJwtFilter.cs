using Ecommerce.Common;
using Ecommerce.Common.JWT;
using Ecommerce.DTO.Models.Common;
using Ecommerce.Services.Interfaces.Unit;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Ecommerce.Filters
{
    public class CheckJwtFilter : IAuthorizationFilter
    {
        #region Private Fields

        private readonly IServiceUnit _service;

        #endregion Private Fields

        #region Constructors

        public CheckJwtFilter(IServiceUnit service)
        {
            _service = service;
            Allows = new List<AccountType>();
        }

        #endregion Constructors

        #region Properties

        public List<AccountType> Allows { get; set; }

        #endregion Properties

        #region Methods

        public void OnAuthorization(AuthorizationFilterContext context)
        {
            string authTokenValue;

            if (context.HttpContext.Request.Headers.Keys.Any(a => a.Equals("Authorization")))
            {
                authTokenValue = context.HttpContext.Request.Headers["Authorization"];

                if (string.IsNullOrWhiteSpace(authTokenValue))
                {
                    context.Result = new UnauthorizedObjectResult(new ResponseWrapper<object>(false, "token missing", null, null));

                    return;
                }
            }
            else
            {
                context.Result = new UnauthorizedObjectResult(new ResponseWrapper<object>(false, "Authorization Header Missing", null, null));

                return;
            }

            var tokenValues = authTokenValue.Split(" ");

            if (tokenValues.Length != 2)
            {
                context.Result = new UnauthorizedObjectResult(new ResponseWrapper<object>() { Success = false, Message = "Invalid Jwt Token" });

                return;
            }

            var token = TokenManger.ValidateToken(tokenValues[1]);

            if (token == null)
            {
                context.Result = new UnauthorizedObjectResult(new ResponseWrapper<object>() { Success = false, Message = "Invalid Jwt Token" });

                return;
            }

            var date = DateTime.UtcNow;

            if (token.ExpiresAt < date)
            {
                context.Result = new UnauthorizedObjectResult(new ResponseWrapper<object>() { Success = false, Message = "Invalid Jwt Token" });

                return;
            }

            // Admin
            if (token.Type == AccountType.Admin)
            {
                if (!_service.User.GetUsers().Any(a => a.Id == token.Id))
                {
                    context.Result = new UnauthorizedObjectResult(new ResponseWrapper<object>() { Success = false, Message = "Invalid Jwt Token" });

                    return;
                }
            }

            // Seller
            if (token.Type == AccountType.Seller)
            {
                if (!_service.Seller.GetSellers().Any(a => a.Id == token.Id))
                {
                    context.Result = new UnauthorizedObjectResult(new ResponseWrapper<object>() { Success = false, Message = "Invalid Jwt Token" });

                    return;
                }
            }

            // Customer
            if (token.Type == AccountType.Customer)
            {
                if (!_service.Customer.GetCustomers().Any(a => a.Id == token.Id))
                {
                    context.Result = new UnauthorizedObjectResult(new ResponseWrapper<object>() { Success = false, Message = "Invalid Jwt Token" });

                    return;
                }
            }
        }

        #endregion Methods
    }
}